using System;
using System.Collections.Generic;

#nullable disable

namespace WBAV6.Models
{
    public partial class Profile
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Cel { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
        public string Estudy { get; set; }
        public string Academy { get; set; }
        public byte? Available { get; set; }
        public string PlaceAt { get; set; }
        public byte? DtVisible { get; set; }
        public string Keyword1 { get; set; }
        public string Keyword2 { get; set; }
        public string Keyword3 { get; set; }
        public string Keyword4 { get; set; }
        public string Keyword5 { get; set; }

        public virtual User User { get; set; }
    }
}
