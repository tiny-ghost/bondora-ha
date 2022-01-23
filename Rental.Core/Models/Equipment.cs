using System;
using System.Collections.Generic;
using System.Text;

namespace Rental.Core.Models
{
    public class Equipment:BaseModel
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public ICollection<Order>  Orders { get; set; }
    }
}
