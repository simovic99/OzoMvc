using System;
using System.Collections.Generic;

namespace OzoMvc.Models
{
    public partial class Oprema
    {
        public Oprema()
        {
            PosaoOprema = new HashSet<PosaoOprema>();
            Transakcija = new HashSet<Transakcija>();
        }

        public int InventarniBroj { get; set; }
        public string Naziv { get; set; }
        public double? KupovnaVrijednost { get; set; }
        public double? KnjigovodstvenaVrijednost { get; set; }
        public double VrijemeAmortizacije { get; set; }
        public int VrstaId { get; set; }
        public int ReferentniId { get; set; }
        public int SkladisteId { get; set; }

        public virtual ReferentniTip Referentni { get; set; }
        public virtual Skladište Skladiste { get; set; }
        public virtual VrstaOpreme Vrsta { get; set; }
        public virtual ICollection<PosaoOprema> PosaoOprema { get; set; }
        public virtual ICollection<Transakcija> Transakcija { get; set; }
    }
}
