using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public string Model { get; set; }
        [Required]
        public double Mileage { get; set; }
        [Required]
        public double BrokerPrice { get; set; }

        public double AddedCost { get; set; }
        [Required]
        public ApplicationUser Broker { get; set; }
        [Required]
        public string BrokerId { get; set; }

        public ApplicationUser Salesman { get; set; }
        public string SalesmanId { get; set; }

        public Customer Customer { get; set; }
        public int? CustomerId { get; set; }

        public double? SoldPrice { get; set; }

  





    }
}
