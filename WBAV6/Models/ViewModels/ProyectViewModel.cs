using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WBAV6.Models.ViewModels
{
    public class ProyectViewModel
    {
        [Required]
        [Display(Name = "Titulo")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Descripción")]
        public string Description { get; set; }
        [Display(Name = "Palabra clave")]
        public string Keyword { get; set; }
        [Display(Name = "Documento")]
        public string Document { get; set; }
    }
}
