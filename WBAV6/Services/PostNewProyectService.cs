using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WBAV6.Models;
using WBAV6.Models.ViewModels;

namespace WBAV6.Services
{
    public class PostNewProyectService : IPostNewProyectService
    {
        private readonly WBAV6Context _context;
        public PostNewProyectService(WBAV6Context context)
        {
            _context = context;
        }

        public void New(ProyectViewModel model, int? id)
        {
            try
            {
                var proyect = new Proyect()
                {
                    Title = model.Title,
                    Description = model.Description,
                    Document = model.Document,
                    Keyword = model.Keyword,
                };
                _context.Add(proyect);
                _context.SaveChanges();
                var proyectAux = _context.Proyects.Where(
                    a => a.Title == proyect.Title &&
                    a.Description == proyect.Description &&
                    a.Document == proyect.Document &&
                    a.Keyword == proyect.Keyword).ToList();
                var user = _context.Users.Find(id);
                user.IdProyect = proyectAux[0].Id;
                _context.Entry(user).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Algo salio mal :(", e);
            }
        }
    }
}
