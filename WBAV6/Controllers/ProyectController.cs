using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WBAV6.Helpers;
using WBAV6.Models;
using WBAV6.Models.ViewModels;
using WBAV6.Services;

namespace WBAV6.Controllers
{
    [ValidationHelper]
    [RoleHelper("student")]
    public class ProyectController : Controller
    {
        private readonly WBAV6Context _context;
        private readonly IPostNewProyectService _postNewProyectService;
        public ProyectController (WBAV6Context context, IPostNewProyectService postNewProyectService)
        {
            _context = context;
            _postNewProyectService = postNewProyectService;
        }
        public async Task<IActionResult> MyProyect(Proyect model, int? id)
        {
            User userSession = HttpContext.Session.GetUserSession();
            if (id == null || id != userSession.Id)
            {
                return Redirect("~/Home");
            }
            var userAux = _context.Users.Find(id);
            model = await _context.Proyects.Where(a=>a.Id == userAux.IdProyect).Include(a=>a.Users).FirstOrDefaultAsync();
            return View(model);
        }
        public IActionResult New(int? id)
        {
            User userSession = HttpContext.Session.GetUserSession();
            if (id == null || id != userSession.Id)
            {
                return Redirect("~/Home");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(ProyectViewModel model ,int? id)
        {
            User userSession = HttpContext.Session.GetUserSession();
            if (id == null || id != userSession.Id)
            {
                return Redirect("~/Home");
            }
            if (ModelState.IsValid)
            {
                _postNewProyectService.New(model, id);
                return Redirect("~/Proyect/MyProyect/" + id);
            }
            return View(model);
        }
        public IActionResult Edit(int? id)
        {
            User userSession = HttpContext.Session.GetUserSession();
            if (id == null || id != userSession.Id)
            {
                return Redirect("~/Home");
            }
            var proyect = _context.Users.Where(a=>a.Id == id).Include(a=>a.IdProyectNavigation).FirstOrDefault();
            if (proyect.IdProyect != null)
            {
                ProyectViewModel proyectVM = new()
                {
                    Title = proyect.IdProyectNavigation.Title,
                    Description = proyect.IdProyectNavigation.Description,
                    Document = proyect.IdProyectNavigation.Document,
                    Keyword = proyect.IdProyectNavigation.Keyword,
                };
                return View("~/Views/Proyect/New.cshtml", proyectVM);

            }
            return Redirect("~/Proyect/MyProyect/" + id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProyectViewModel model, int? id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User userSession = HttpContext.Session.GetUserSession();
                    var userAux = _context.Users.Find(id);
                    var result = _context.Proyects.FirstOrDefault(m => m.Id == userAux.IdProyect);
                    result.Title = model.Title;
                    result.Description = model.Description;
                    result.Document = model.Document;
                    result.Keyword = model.Keyword;
                    _context.Entry(result).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Redirect("~/Proyect/MyProyect/" + id);
                }
                catch (Exception e)
                {
                    throw new Exception("Algo salio mal :(", e);
                }
            }
            return View("~/Views/Proyect/New.cshtml", model);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            User userSession = HttpContext.Session.GetUserSession();
            if (id == null || id != userSession.Id)
            {
                return Redirect("~/Home");
            }
            var userAux = _context.Users.Find(id);
            var proyect = await _context.Proyects.Where(a => a.Id == userAux.IdProyect).Include(a=>a.Users).ToListAsync();
            if (proyect != null)
            {
                try
                {
                    foreach (var user in proyect[0].Users)
                    {
                        user.IdProyect = null;
                        _context.Entry(user).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }
                    _context.Remove(proyect[0]);
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    throw new Exception("Algo salio Mal :(", e);
                }
                return Redirect("~/Proyect/MyProyect/" + id);
            }
            return Redirect("~/Proyect/MyProyect/"+id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Join([FromForm] int JoinId, int? id)
        {
            User userSession = HttpContext.Session.GetUserSession();
            if (id == null || id != userSession.Id)
            {
                return Redirect("~/Home");
            }
            try
            {
                var user = _context.Users.Find(id);
                user.IdProyect = JoinId;
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Redirect("~/Proyect/MyProyect/" + id);
            }
            catch (Exception e)
            {
                return Redirect("~/Proyect/MyProyect/" + id);
            }
        }
        public async Task<IActionResult> Exit(int? id)
        {
            User userSession = HttpContext.Session.GetUserSession();
            if (id == null || id != userSession.Id)
            {
                return Redirect("~/Home");
            }
            try
            {
                var user = _context.Users.Find(id);
                user.IdProyect = null;
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Redirect("~/Proyect/MyProyect/" + id);
            }
            catch (Exception e)
            {
                return Redirect("~/Proyect/MyProyect/" + id);
            }
        }
    }
}
