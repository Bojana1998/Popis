using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Popis.Models
{
    public class Lokacija
    {
        
        [DisplayName("ID Lokacije")]
        [Key]
        public int id { get; set; }

        [DisplayName("Naziv lokacije")] 
        [Required]
        public string Naziv { get; set; }

        public IList<Sredstvo> Sredstvo { get; set; }
        public IList<Inventar> Inventar { get; set; }
        
    }
}
