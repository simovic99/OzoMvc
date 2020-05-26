using System;
using System.Collections.Generic;

namespace OzoMvc.Models
{
    public partial class Grad
    {
        public Grad()
        {
            Mjesto = new HashSet<Mjesto>();
        }

        public int Id { get; set; }
        public string Naziv { get; set; }

        public virtual ICollection<Mjesto> Mjesto { get; set; }
    }
}
