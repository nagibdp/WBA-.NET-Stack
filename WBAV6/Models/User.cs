using System;
using System.Collections.Generic;

#nullable disable

namespace WBAV6.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public int? IdProyect { get; set; }
        public string Name { get; set; }
        public string LastNameF { get; set; }
        public string LastNameM { get; set; }
        public string Pass { get; set; }
        public string Email { get; set; }
        public string Career { get; set; }
        public string Role { get; set; }

        public virtual Proyect IdProyectNavigation { get; set; }
        public virtual Profile Profile { get; set; }
    }
}
