using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCapstone.Models.ViewModels
{
    public class CustomerIndexViewModel
    {
        public List<Customer> Customer { get; set; }
        public bool IsOffice { get; set; }
    }
}
