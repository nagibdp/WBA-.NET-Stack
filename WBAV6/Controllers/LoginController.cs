using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WBAV6.Models;
using WBAV6.Helpers;
using WBAV6.Models.ViewModels;

namespace WBAV6.Controllers
{
    public class LoginController : Controller
    {
        private readonly WBAV6Context _context;
        //private readonly string sCon = "Server=.; Database=WBAV6; Trusted_Connection=true;";

        public LoginController(WBAV6Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> Signin()
        {
            //var users = _context.Users.Include(a => a.IdProyectNavigation);
            return View(/*await users.ToListAsync()*/);            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signin(UserViewModel model)
        {
            if (!(model.Email.Contains("@ipn.mx") || model.Email.Contains("@alumno.ipn.mx")))
            {
                ModelState.AddModelError(nameof(model.Email), "Introduzca un email institucional valido");
            }

            if (ModelState.IsValid)
            {
                var user = _context.Users.SingleOrDefault(a => a.Email == model.Email);
                await _context.SaveChangesAsync();
                if (user == null || BCrypt.Net.BCrypt.Verify(model.Pass, user.Pass) == false)
                {
                    ModelState.AddModelError(nameof(model.Pass), "Credenciales incorrectas, intentelo de nuevo");
                    return View(model);
                }

                HttpContext.Session.SetObjectAsJson("user", user);
                return RedirectToAction("Index", "Home");
                //using (SqlConnection cn = new(sCon))
                //{
                //    SqlCommand cmd = new("spValidate", cn);
                //    cmd.Parameters.AddWithValue("email", model.Email);
                //    cmd.Parameters.AddWithValue("pass", model.Pass);
                //    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //    cn.Open();
                //    id = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                //}
            }
            return View(model);
        }

        public IActionResult Signup()
        {
            //ViewData[""] = new SelectList(context.tabla, "valor que se va", "valor que se muestra" )
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signup(UserViewModel model)
        {
            string role = "";
            if (model.Email.Contains("@ipn.mx"))
            {
                role = "teacher";
            }
            else if (model.Email.Contains("@alumno.ipn.mx"))
            {
                role = "student";
            }
            else
            {
                ModelState.AddModelError(nameof(model.Email), "Debe introducir un correo institucional valido");
            }

            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    Name = model.Name,
                    LastNameF = model.LastNameF,
                    LastNameM = model.LastNameM,
                    Pass = BCrypt.Net.BCrypt.HashPassword(model.Pass),
                    Email = model.Email,
                    Career = model.Career,
                    Role = role
                };
                _context.Add(user);
                await _context.SaveChangesAsync();
                return Redirect(nameof(Signin));
            }
            //ViewData[""] = new SelectList(context.tabla, "valor que se va", "valor que se muestra" )
            return View(model);
        }
        public IActionResult Logout()
        {
            //ViewData[""] = new SelectList(context.tabla, "valor que se va", "valor que se muestra" )
            HttpContext.Session.Clear();
            return RedirectToAction("Signin","Login");
        }
    }
}
