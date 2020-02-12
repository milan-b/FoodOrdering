using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Prilog
    {
        public int PrilogId { get; set; }
        public string Vrijednost { get; set; }
        public int Varijanta { get; set; }

        public virtual ICollection<HranaPrilog> Hrana { get; set; }
    }
}
