using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WBAV6.Models.ViewModels
{
    public class UserViewModel
    {
        [Display(Name = "Nombre")]
        public string Name { get; set; }
        [Display(Name = "Apellido paterno")]
        public string LastNameF { get; set; }
        [Display(Name = "Apellido materno")]
        public string LastNameM { get; set; }
        [Required]
        [Display(Name = "Contraseña")]
        public string Pass { get; set; }
        [Required]
        [Display(Name = "Email institucional")]
        public string Email { get; set; }
        [Display(Name = "Carrera")]
        public string Career { get; set; }
    }
}
