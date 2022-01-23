using Rental.Core.Contracts;
using Rental.Core.Contracts.Service;
using Rental.Core.Models;
using Rental.Core.ViewModel;


namespace Rental.Service
{
    //Entire class has to be covered by test as this generates critical data
    public class InvoiceService : IInvoiceService
    {
        private const decimal ONE_TIME_FEE = 100.00m;
        private const decimal PREMIUM_FEE = 60.00m;
        private const decimal REGULAR_FEE = 40.00m;

        private readonly IUnitOfWork _unitOfWork;

        public InvoiceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<InvoiceViewModel> CreateInvoiceFromOrderAsync(int orderId)
        {
            var order = await _unitOfWork.Orders.GetByIdASync(orderId);
            var customer = await _unitOfWork.Customers.GetAsync(order.CustomerId);
            var totalLoyalty = CalculateRoyaltyPoints(order);

            var rentalItem = CalculateRentalFee(order);
            var totalPrice = 0.00m;
            rentalItem.ForEach(item => totalPrice += item.Fee);
            var invoice = new InvoiceViewModel
            {
                Title = $"Order {order.Id} for {customer.Name}",
                RentalItem = rentalItem,
                TotalPrice = totalPrice,
                TotalRoyaltyEarned = totalLoyalty,

            };

            return invoice;

        }

        private List<RentalItemViewModel> CalculateRentalFee(Order order)
        {
            var result = new List<RentalItemViewModel>();
            foreach (var item in order.RentItems)
            {
                (string name , decimal fee) = item.Equipment.Type switch
                {
                    "Heavy" => CalculateHeavy(item),
                    "Regular" => CalculateRegular(item),
                    "Specialized" => CalculateSpecialized(item),
                    _ => new ()
                };

               result.Add(new() { Name = name,Fee = fee });
            }

            return result;
        }

        private static (string name,decimal fee) CalculateHeavy(RentalItem item)
        {
            var equipName = item.Equipment.Name;

            var fee = (item.DaysOfRental * PREMIUM_FEE) + ONE_TIME_FEE;

            return (equipName, fee);

        }

        private static (string name, decimal fee) CalculateRegular(RentalItem item)
        {

            var equipName = item.Equipment.Name;
            var fee = 0.00m;

            if (item.DaysOfRental is <= 2 and not 0)
            {
                fee = (item.DaysOfRental * PREMIUM_FEE);
            }
            
            if(item.DaysOfRental > 2)
            {
                fee = (2 * PREMIUM_FEE) + ((item.DaysOfRental - 2) *  REGULAR_FEE);
            }

            fee += ONE_TIME_FEE;

            return (equipName, fee);

        }

        private static (string name, decimal fee) CalculateSpecialized(RentalItem item)
        {

            var equipName = item.Equipment.Name;
            var fee = 0.00m;

            if (item.DaysOfRental is <= 3 and not 0)
            {
                fee = (item.DaysOfRental * PREMIUM_FEE);
            }

            if (item.DaysOfRental > 3)
            {
                fee = (3 * PREMIUM_FEE) + ((item.DaysOfRental - 3) * REGULAR_FEE);
            }

            fee += ONE_TIME_FEE;

            return (equipName, fee);

        }

        private int CalculateRoyaltyPoints(Order order)
        {
            var total = 0;
            foreach (var item in order.RentItems)
            {
                int point = item.Equipment.Type switch
                {
                    "Heavy" => 2,
                    _ => 1
                };
                total += point;
            }

            return total;
        }
    }
}
