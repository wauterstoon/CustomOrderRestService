using System;
using System.Collections.Generic;

namespace CustomerOrderRESTService.ASP.NET.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<BusinessLayer.Models.Order> Orders { get; set; }
    }
}
