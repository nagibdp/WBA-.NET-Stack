using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WBAV6.Models;
using WBAV6.Models.ViewModels;
using WBAV6.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace WBAV6.Controllers
{
    [ValidationHelper]
    public class ProfileController : Controller
    {
        private readonly WBAV6Context _context;            
        public ProfileController(WBAV6Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int? id)
        {
            try
            {
                User userSession = HttpContext.Session.GetUserSession();
                var profile = _context.Profiles.Where(m => m.UserId == id).Include(a=>a.User).FirstOrDefault();
                //await _context.SaveChangesAsync();
                if (profile != null)
                {
                    return View(profile);
                }
            }
            catch(Exception e)
            {
                throw new Exception("Algo salio mal :(", e);
            }
            return View();
        }
        public IActionResult New()
        {
            User userSession = HttpContext.Session.GetUserSession();
            if (_context.Profiles.FirstOrDefault(a => a.UserId == userSession.Id) != null)
            {
                return RedirectToAction("Index", "Profile");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(ProfileViewModel model, int? id)
        {
            if (model.PictureFile != null)
            {
                using FileStream fs = System.IO.File.Create("wwwroot/img/"+ id + ".png");
                await model.PictureFile.CopyToAsync(fs);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    
                    var profile = new Profile()
                    {
                        UserId = id,
                        Cel = model.Cel,
                        Picture = model.PictureFile.FileName,
                        Description = model.Description,
                        Estudy = model.Estudy,
                        Academy = model.Academy,
                        Available = Convert.ToByte(model.Available),
                        PlaceAt = model.PlaceAt,
                        DtVisible = Convert.ToByte(model.DtVisible),
                        Keyword1 = model.Keyword1,
                        Keyword2 = model.Keyword2,
                        Keyword3 = model.Keyword3,
                        Keyword4 = model.Keyword4,
                        Keyword5 = model.Keyword5
                    };
                    _context.Add(profile);
                    await _context.SaveChangesAsync();
                    return Redirect("~/Profile/Index/"+id);
                }
                catch(Exception e)
                {
                    throw new Exception("Algo salio mal :(", e);
                }
            }
            return View(model);
        }
        public IActionResult Edit(int? id)
        {
            var profile = _context.Profiles.FirstOrDefault(a => a.UserId == id);
            if(profile != null)
            {
                ProfileViewModel profileVM = new()
                {
                    Cel = profile.Cel,
                    Picture = profile.Picture,
                    Description = profile.Description,
                    Estudy = profile.Estudy,
                    Academy = profile.Academy,
                    Available = Convert.ToBoolean(profile.Available),
                    PlaceAt = profile.PlaceAt,
                    DtVisible = Convert.ToBoolean(profile.DtVisible),
                    Keyword1 = profile.Keyword1,
                    Keyword2 = profile.Keyword2,
                    Keyword3 = profile.Keyword3,
                    Keyword4 = profile.Keyword4,
                    Keyword5 = profile.Keyword5
                };
                return View("~/Views/Profile/New.cshtml", profileVM);

            }
            return RedirectToAction("Index", "Profile");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProfileViewModel model, int? id)
        {
            if (model.PictureFile != null)
            {
                using FileStream fs = System.IO.File.Create("wwwroot/img/" + id + ".png");
                await model.PictureFile.CopyToAsync(fs);
            }
            if (ModelState.IsValid)
            {
                try
                {

                    User userSession = HttpContext.Session.GetUserSession();
                    var result = _context.Profiles.FirstOrDefault(m=> m.UserId == userSession.Id);
                    result.Cel = model.Cel;
                    result.Picture = model.PictureFile.FileName;
                    result.Description = model.Description;
                    result.Estudy = model.Estudy;
                    result.Academy = model.Academy;
                    result.Available = Convert.ToByte(model.Available);
                    result.PlaceAt = model.PlaceAt;
                    result.DtVisible = Convert.ToByte(model.DtVisible);
                    result.Keyword1 = model.Keyword1;
                    result.Keyword2 = model.Keyword2;
                    result.Keyword3 = model.Keyword3;
                    result.Keyword4 = model.Keyword4;
                    result.Keyword5 = model.Keyword5;
                    _context.Entry(result).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Redirect("~/Profile/Index/"+id);
                }
                catch (Exception e)
                {
                    throw new Exception("Algo salio mal :(", e);
                }
            }
            return View("~/Views/Profile/New.cshtml",model);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            User userSession = HttpContext.Session.GetUserSession();
            var profile = _context.Profiles.FirstOrDefault(a => a.UserId == userSession.Id);
            if (profile != null)
            {
                try
                {
                    _context.Remove(profile);
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    throw new Exception("Algo salio Mal :(", e);
                }
                return Redirect("~/Profile/Index/" + id);
            }
            return RedirectToAction("Index", "Profile");
        }
    }
}
