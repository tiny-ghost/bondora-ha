using Rental.Core.Contracts;
using Rental.Core.Contracts.Service;
using Rental.Core.Models;

namespace Rental.Service
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Order> GetByIdAsync(int id)
        {
             return await _unitOfWork.Orders.GetByIdASync(id);
        }
        public async Task<Order> PlaceOrderForCustomerASync(Order order)
        {
            await _unitOfWork.Orders.CreateOrderAsync(order);

            //var rentItems = new List<RentalItem>();
            //foreach (var item in order.RentItems)
            //{
                
            //    var rental = new RentalItem
            //    {
            //        DaysOfRental = item.DaysOfRental,
            //        EquipmentId = item.EquipmentId,
            //        OrderId = order.Id
            //    };
            //    rentItems.Add(rental);
            //}
            foreach (var item in order.RentItems)
            {
                await _unitOfWork.RentalItems.AddAsync(item);
            }
            
            
            return order;

        }
    }
}
