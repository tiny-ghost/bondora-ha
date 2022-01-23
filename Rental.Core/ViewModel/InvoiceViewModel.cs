using System.Collections.Generic;

namespace Rental.Core.ViewModel
{
    public class InvoiceViewModel
    {
        public string Title { get; set; }

        public List<RentalItemViewModel> RentalItem { get; set; }

        public decimal TotalPrice { get; set; }
        public int TotalRoyaltyEarned { get; set; }
    }

    public class RentalItemViewModel
    {
        public string Name { get; set; }
        public decimal Fee { get; set; }
    }
}
