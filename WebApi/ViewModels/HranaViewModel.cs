using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModels
{
    public class HranaViewModel
    {
        public int HranaId { get; set; }
        public string Naziv { get; set; }
        public bool Stalna { get; set; }
        public List<HranaPrilogViewModel> Prilozi { get; set; }
        public bool Narucena { get; set; }
    }
}
