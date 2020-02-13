using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models
{
    public class Komentar
    {
        public int KomentarId { get; set; }
        [Required]
        public int HranaId { get; set; }
        public string? Slika { get; set; }
        [Required]
        public int KorisnikId { get; set; }
        [Required]
        public Korisnik Korisnik { get; set; }

        //public ICollection<SavedBook> BookUsers { get; set; }
    }
}
