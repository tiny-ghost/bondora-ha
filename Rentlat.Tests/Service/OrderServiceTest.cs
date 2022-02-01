using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Rental.Core.Models;
using Rental.Persistence;
using Rental.Service;
using Rentlat.Tests;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Rental.Tests.Service
{
    public class OrderServiceTest :RepositoryTest, IDisposable
    {

        private readonly DbConnection _connection;

        public OrderServiceTest()  : base(new DbContextOptionsBuilder<RentalContext>()
                 .UseSqlite(CreateInMemoryDatabase()).Options)
        {

            _connection = RelationalOptionsExtension.Extract(ContextOptions).Connection;
        }

        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();
            return connection;
        }

        public void Dispose() => _connection.Dispose();


        [Fact]
        public async Task PlaceOrderForCustomerAsync_WhenCalled_ShouldCreateValidOrderWithRentItems()
        {
            //Arrange
            using var context = new RentalContext(ContextOptions);
            var unitOfWork = new UnitOfWork(context);
            var _orderService = new OrderService(unitOfWork);
            var expected = 1;
            var order = new Order
            {
                CustomerId = 1,
                RentItems = new List<RentalItem>
                {
                    new RentalItem()
                    {
                        DaysOfRental = 3,
                        EquipmentId = 2,
                    }
                }
            };

            //Act
            await _orderService.PlaceOrderForCustomerASync(order);
            var result = await _orderService.GetByIdAsync(1);

            //Assert
            Assert.Equal(result.RentItems.Count(), expected);
        }

        [Fact]
        public async Task PlaceOrderForCustomerAsync_WhenCalled_ShouldCreateValidOrderWithCorrectRentItemName()
        {
            //Arrange
            using var context = new RentalContext(ContextOptions);
            var unitOfWork = new UnitOfWork(context);
            var _orderService = new OrderService(unitOfWork);
            var expected = "KamAZ truck";
            var order = new Order
            {
                CustomerId = 1,
                RentItems = new List<RentalItem>
                {
                    new RentalItem()
                    {
                        DaysOfRental = 3,
                        EquipmentId = 2,
                    }
                }
            };

            //Act
            await _orderService.PlaceOrderForCustomerASync(order);

            var result = await _orderService.GetByIdAsync(1);
            var actualName = result.RentItems.First().Equipment.Name;

            //Assert
            Assert.Equal(actualName, expected);
        }
    }
}
