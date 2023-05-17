using System;
using System.Collections.Generic;

#nullable disable

namespace WBAV6.Models
{
    public partial class Proyect
    {
        public Proyect()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Keyword { get; set; }
        public string Document { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
