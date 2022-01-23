
using System.Collections.Generic;


namespace Rental.Core.Models
{
    public class Order:BaseModel
    {

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<RentalItem> RentItems { get; set; } = new List<RentalItem>();
    }
}
