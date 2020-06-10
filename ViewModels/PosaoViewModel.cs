using System;
using System.Collections.Generic;
using OzoMvc.Models;

namespace OzoMvc.ViewModels
{
    public class PosaoViewModel

    {
      

        public int Id { get; set; }
        public DateTime? Vrijeme { get; set; }
        public string UslugaNaziv { get; set; }
        public string MjestoNaziv { get; set; }
        public double Cijena { get; set; }
        public double Troskovi { get; set; }
       
        
        public virtual ICollection<ZaposlenikPosao> ZaposlenikPosao { get; set; }
        public virtual ICollection<PosaoOprema> PosaoOprema { get; set; }
        public virtual Usluga UslugaNavigation { get; set; }
        public virtual Mjesto MjestoNavigation { get; set; }


    }
}
