using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models
{
    public class Ocjena
    {
        public int OcjenaId { get; set; }
        [Required]
        public int Vrijednost { get; set; }
        [Required]
        public int HranaId { get; set; }
        [Required]
        public int KorisnikId { get; set; }
        [Required]
        public Korisnik Korisnik { get; set; }

        //public ICollection<SavedBook> BookUsers { get; set; }
    }
}
