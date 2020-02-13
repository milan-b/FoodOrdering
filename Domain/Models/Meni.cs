using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Meni
    {
        public int MeniId { get; set; }
        public DateTime Datum { get; set; }
        public ICollection<HranaMeni> Hrana { get; set; }
        public ICollection<Narudzba> Narudzbe { get; set; }

    }
}
