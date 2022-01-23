using System;
using System.Collections.Generic;
using System.Text;

namespace Rental.Core.Models
{
    public class Customer:BaseModel
    {
        public string Name { get; set; }
        public int LoyaltyPoints { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
