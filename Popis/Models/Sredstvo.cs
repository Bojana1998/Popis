using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Popis.Models
{
    public class Sredstvo
    {
        [DisplayName("ID Sredstva")]
        [Key]
        public int id { get; set; }
    
        [Required]
        public string Naziv { get; set; }

        [Required]
        public string Opis { get; set; }
       
        [DisplayName("Datum nabavke")]
        [Required]
        public DateTime DatumNabavke { get; set; }

        [DisplayName("Datum isteka")]
        [Required]
        public DateTime DatumIsteka { get; set; }
      
        [ForeignKey("Korisnik")]
        public int KorisnikId { get; set; }

        [ForeignKey("Lokacija")]
        public int LokacijaId { get; set; }
        

        public  Korisnik Korisnik { get; set; }
        public Lokacija Lokacija { get; set; }
       
    }
}
