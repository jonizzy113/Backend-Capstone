using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCapstone.Models.ViewModels
{
    public class SalesmanIndexViewModel
    {
        public List<ApplicationUser> Salesman { get; set; }
        public bool IsOffice { get; set; }
    }
}
