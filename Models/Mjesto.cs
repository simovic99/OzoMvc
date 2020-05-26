using System;
using System.Collections.Generic;

namespace OzoMvc.Models
{
    public partial class Mjesto
    {
        public Mjesto()
        {
            Skladište = new HashSet<Skladište>();
            Zaposlenik = new HashSet<Zaposlenik>();
        }

        public int Id { get; set; }
        public string Naziv { get; set; }
        public int GradId { get; set; }

        public virtual Grad Grad { get; set; }
        public virtual ICollection<Skladište> Skladište { get; set; }
        public virtual ICollection<Zaposlenik> Zaposlenik { get; set; }
    }
}
