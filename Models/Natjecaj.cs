using System;
using System.Collections.Generic;

namespace OzoMvc.Models
{
    public partial class Natjecaj
    {
        public Natjecaj()
        {
            NatjecajPonudjivac = new HashSet<NatjecajPonudjivac>();
        }

        public int Id { get; set; }
        public string Opis { get; set; }
        public DateTime TrajanjeOd { get; set; }
        public DateTime TrajanjeDo { get; set; }
        public double Cijena { get; set; }
        public int UslugaId { get; set; }

        public virtual Usluga Usluga { get; set; }
        public virtual ICollection<NatjecajPonudjivac> NatjecajPonudjivac { get; set; }
    }
}
