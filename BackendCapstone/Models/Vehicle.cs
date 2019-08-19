using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCapstone.Models
{
    [Authorize]
    public class Vehicle
    {
        [Key]
        public int VehicleId { get; set; }
        [Required]
        public string Make { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:N}")]
        public double Mileage { get; set; }
        [Required]
        [DisplayFormat(DataFormatString ="{0:C}")]
        [Display(Name = "Broker Price")]
        public double BrokerPrice { get; set; }

        [Display(Name = "Added Costs")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double AddedCost { get; set; }
        [Required]
        public ApplicationUser Broker { get; set; }
        [Required]
        [Display(Name ="Broker")]
        public string BrokerId { get; set; }

        public ApplicationUser Salesman { get; set; }
        [Display(Name ="Customer")]
        public string SalesmanId { get; set; }

        public Customer Customer { get; set; }
        [Display(Name ="Customer")]
        public int? CustomerId { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name ="Sold Price")]
        public double? SoldPrice { get; set; }
        [NotMapped]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double? Profit
        {
            get
            {
                return SoldPrice - AddedCost - BrokerPrice;
            }
        }


  





    }
}
