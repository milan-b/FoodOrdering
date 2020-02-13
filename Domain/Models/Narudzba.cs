using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models
{
    public class Narudzba
    {
        public int NarudzbaId { get; set; }
        [Required]
        public int MeniId { get; set; }
        [Required]
        public int HranaId { get; set; }
        [Required]
        public int KorisnikId { get; set; }
        [Required]
        public Korisnik Korisnik { get; set; }
        public int PrilogId { get; set; }
        public int Dostavljena { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        //public ICollection<SavedBook> BookUsers { get; set; }
    }
}
