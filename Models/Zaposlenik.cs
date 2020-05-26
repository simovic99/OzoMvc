using System;
using System.Collections.Generic;

namespace OzoMvc.Models
{
    public partial class Zaposlenik
    {
        public Zaposlenik()
        {
            CertifikatZaposlenik = new HashSet<CertifikatZaposlenik>();
            KategorijaZaposlenik = new HashSet<KategorijaZaposlenik>();
            ZaposlenikPosao = new HashSet<ZaposlenikPosao>();
        }

        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime? DatumRodjenja { get; set; }
        public double? MjesecniTrosak { get; set; }
        public int? MjestoId { get; set; }

        public virtual Mjesto Mjesto { get; set; }
        public virtual ICollection<CertifikatZaposlenik> CertifikatZaposlenik { get; set; }
        public virtual ICollection<KategorijaZaposlenik> KategorijaZaposlenik { get; set; }
        public virtual ICollection<ZaposlenikPosao> ZaposlenikPosao { get; set; }
    }
}
