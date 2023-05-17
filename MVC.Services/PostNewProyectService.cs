using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using WBAV6.Models;
using WBAV6.Models.ViewModels;

namespace MVC.Services
{
    public class PostNewProyectService : IPostNewProyectService
    {
        private readonly WBAV6Context _context;

        public PostNewProyectService(WBAV6Context context)
        {
            _context = context;
        }

        public async void Task<New>(ProyectViewModel model, int? id)
        {
            var proyect = new Proyect()
            {
                Title = model.Title,
                Description = model.Description,
                Document = model.Document,
                Keyword = model.Keyword,
            };
            _context.Add(proyect);
            await _context.SaveChangesAsync();
            var proyectAux = await _context.Proyects.Where(
                a => a.Title == proyect.Title &&
                a.Description == proyect.Description &&
                a.Document == proyect.Document &&
                a.Keyword == proyect.Keyword).ToListAsync();
            var user = _context.Users.Find(id);
            user.IdProyect = proyectAux[0].Id;
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
