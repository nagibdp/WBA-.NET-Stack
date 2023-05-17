using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WBAV6.Models.ViewModels
{
    public class ProfileViewModel
    {
        [Phone]
        public int? UserId { get; set; }
        [Display(Name = "Celular")]
        public string Cel { get; set; }
        [Display(Name = "Foto de perfil")]
        public IFormFile PictureFile { get; set; }
        public string Picture { get; set; }

        [Display(Name = "Descripción")]
        public string Description { get; set; }
        [Display(Name = "Estudios")]
        public string Estudy { get; set; }
        [Display(Name = "Academia")]
        public string Academy { get; set; }
        [Display(Name = "Disponible")]
        public bool Available { get; set; }
        [Display(Name = "Lugar de atención")]
        public string PlaceAt { get; set; }
        [Display(Name = "Datos visibles")]
        public bool DtVisible { get; set; }
        [Display(Name = "Palabra clave 1")]
        public string Keyword1 { get; set; }
        [Display(Name = "Palabra clave 2")]
        public string Keyword2 { get; set; }
        [Display(Name = "Palabra clave 3")]
        public string Keyword3 { get; set; }
        [Display(Name = "Palabra clave 4")]
        public string Keyword4 { get; set; }
        [Display(Name = "Palabra clave 5")]
        public string Keyword5 { get; set; }

        public virtual User User { get; set; }
    }
}
