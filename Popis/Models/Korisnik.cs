using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Popis.Models
{
    public class Korisnik
    {

        [DisplayName("ID Korisnika")]
        [Key]
        public int id { get; set; }

        [DisplayName("Ime")]
        [Required]
        public string Naziv { get; set; }

        public string Telefon { get; set; }

        [Required]
        public string Email { get; set; }

        public IList<Sredstvo> Sredstvo { get; set; }
    }
}
