using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModels
{
    public class MeniViewModel
    {
        public int MeniId { get; set; }
        public List<HranaMeniViewModel> Hrana { get; set; }
    }
}
