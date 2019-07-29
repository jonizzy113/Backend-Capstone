using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCapstone.Models.ViewModels
{
    public class VehicleCreateViewModel
    {
        public Vehicle Vehicle { get; set; }
        public List<ApplicationUser> Brokers { get; set; }

        public List<SelectListItem> BrokersList
        {
            get
            {
                if (Brokers == null)
                {
                    return null;
                }
                return Brokers
                       //this select converts the cohort types in the list to SelectListItems
                       .Select(c => new SelectListItem(c.FullName, c.Id.ToString()))
                       .ToList();
            }



            //public List<SelectListItem> BorkerList =>
            //    Brokers?.Select(b => new SelectListItem(b.FullName, b.Id, b.UserType.Type == "Broker".ToString())).ToList();


        }
    }
}
