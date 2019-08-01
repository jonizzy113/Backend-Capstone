using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCapstone.Models.ViewModels
{
    public class VehicleEditViewModel
    {
        public Vehicle Vehicle { get; set; }
        public List<ApplicationUser> Salesman { get; set; }
        public List<ApplicationUser> Broker { get; set; }
        public List<Customer> Customer { get; set; }
        public List<SelectListItem> BrokerList
        {
            get
            {
                if (Broker == null)
                {
                    return null;
                }
                return Broker
                       .Select(c => new SelectListItem(c.FullName, c.Id.ToString()))
                       .ToList();
            }
        }
        public List<SelectListItem> SalesmanList
        {
            get
            {
                if (Salesman == null)
                {
                    return null;
                }
                return Salesman
                       .Select(s => new SelectListItem(s.FullName, s.Id.ToString()))
                       .ToList();
            }
        }
        public List<SelectListItem> CustomerList
        {
            get
            {
                if (Customer == null)
                {
                    return null;
                }
                return Customer
                       .Select(c => new SelectListItem(c.FullName, c.CustomerId.ToString()))
                       .ToList();
            }
        }
    }
}
