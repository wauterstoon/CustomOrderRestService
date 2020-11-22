using CustomerOrderRESTService.BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerOrderRESTService.ASP.NET.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int Amount { get; set; }
        public ProductType Product { get; set; }
    }
}
