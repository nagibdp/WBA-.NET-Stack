using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WBAV6.Models;
using WBAV6.Models.ViewModels;

namespace WBAV6.Services
{
    public interface IPostNewProyectService
    {
        public void New(ProyectViewModel model, int? id);

    }
}
