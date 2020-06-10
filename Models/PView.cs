using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OzoMvc.Models
{
    public class PView
    {
        /*SELECT [zaposlenik_id]
      ,[posao_id]
      ,[ime]
      ,[prezime]
      ,[inventarni_broj]
      ,[naziv]
      ,[vrijeme]
      ,[id]
      ,[cijena]
      ,[troskovi]
      ,[usluga_id]
      ,[mjesto_id]
  FROM [dbo].[PosloviView]*/
        public int Id { get; set; }
        public int Zaposlenik_id { get; set; }
       
        public int Usluga_id { get; set; }
        public int Mjesto_id { get; set; }
        public int Troskovi { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime? Vrijeme { get; set; }
        public string Naziv{ get; set; }
        public int Posao_id { get; set; }
        public int Inventarni_broj { get; set; }
        public double Cijena { get; set; }

    }
}
