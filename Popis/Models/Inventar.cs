using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Popis.Models
{
    public class Inventar
    {
        [DisplayName("ID Inventara")]
        [Key]
        public int id { get; set; }

        [Required]
        public DateTime Kreirano { get; set; }

         public DateTime Zavrseno { get; set; }

        [ForeignKey("Lokacija")]
        public int LokacijaId { get; set; }

       
        public Lokacija Lokacija { get; set; }
    }
}
