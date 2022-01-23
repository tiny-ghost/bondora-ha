
using System.Collections.Generic;


namespace Rental.Core.Models
{
    public class Order:BaseModel
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<Equipment> Equipment { get; set; }

    }
}
