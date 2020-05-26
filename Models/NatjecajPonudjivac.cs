using System;
using System.Collections.Generic;

namespace OzoMvc.Models
{
    public partial class NatjecajPonudjivac
    {
        public int Id { get; set; }
        public int NatjecajId { get; set; }
        public int PonudjivacId { get; set; }
        public int DobivenNatjecaj { get; set; }

        public virtual Natjecaj Natjecaj { get; set; }
        public virtual Ponudjivaci Ponudjivac { get; set; }
    }
}
