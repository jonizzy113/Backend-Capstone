using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCapstone.Models
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public double Mileage { get; set; }
        public double BrokerPrice { get; set; }
        public double AddedCost { get; set; }

        public ApplicationUser Broker { get; set; }

        public string BrokerId { get; set; }

        public ApplicationUser Salesman { get; set; }
        public string SalesmanId { get; set; }

        public Customer Customer { get; set; }
        public int CustomerId { get; set; }

        public double SoldPrice { get; set; }

  





    }
}
