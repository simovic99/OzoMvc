using System;
using System.Collections.Generic;

namespace OzoMvc.Models
{
    public partial class Ponudjivaci
    {
        public Ponudjivaci()
        {
            NatjecajPonudjivac = new HashSet<NatjecajPonudjivac>();
        }

        public int Id { get; set; }
        public string Naziv { get; set; }

        public virtual ICollection<NatjecajPonudjivac> NatjecajPonudjivac { get; set; }
    }
}
