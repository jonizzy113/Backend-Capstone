using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCapstone.Models.ViewModels
{
    public class BrokerIndexViewModel
    {
        public List<ApplicationUser> Broker { get; set; }
        public bool IsOffice { get; set; }
    }
}
