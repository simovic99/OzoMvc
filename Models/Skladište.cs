using System;
using System.Collections.Generic;

namespace OzoMvc.Models
{
    public partial class Skladište
    {
        public int Id { get; set; }
        public int MjestoId { get; set; }
        public string Naziv { get; set; }

        public virtual Mjesto Mjesto { get; set; }

        public virtual ICollection<Oprema> Oprema { get; set; }
    }
}
