using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Rental.Core.Models;
using Rental.Persistence;
using Rentlat.Tests;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Rental.Tests.Repository
{
    public class InMemoryOrderRepositoryTest : RepositoryTest, IDisposable
    {
        private readonly DbConnection _connection;
        public InMemoryOrderRepositoryTest() : base(new DbContextOptionsBuilder<RentalContext>()
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
        public async Task CreateOrderAsync_WhenCalled_ShouldAddOrderToDb()
        {
            //Arrange
            using var context = new RentalContext(ContextOptions);
            var unitOfWork = new UnitOfWork(context);
            var orderId = 4;
            Order order = new()
            {
                Id = orderId,
                CustomerId = 1,
                RentItems = new List<RentalItem>
                {
                },
            };

            //Act
            await unitOfWork.Orders.CreateOrderAsync(order);
            var result = await unitOfWork.Orders.GetByIdASync(orderId);

            //Assert
            Assert.Equal(result.Id, order.Id);

        }

        [Fact]
        public async Task GetByIdAsync_WithGivenId_Should_ReturnCorrectOrder()
        {
            //Arrange
            using var context = new RentalContext(ContextOptions);
            var unitOfWork = new UnitOfWork(context);
            var orderId = 23;
            Order order = new()
            {
                Id = orderId,
                CustomerId = 1,
                RentItems = new List<RentalItem>
                {
                },
            };

            //Act
            await unitOfWork.Orders.CreateOrderAsync(order);
            var result = await unitOfWork.Orders.GetByIdASync(orderId);

            //Assert
            Assert.Equal(result.Id, orderId);
        }

        [Fact]  
        public async Task TaskGetAllByCustomerAsync_WithCustomerId_Should_ReturnAllCustomerOrders()
        {
            //Arrange
            using var context = new RentalContext(ContextOptions);
            var unitOfWork = new UnitOfWork(context);
            var orderId1 = 23;
            var orderId2 = 15;
            var customerId = 1;
            var expected = 2;

            Order order1 = new()
            {
                Id = orderId1,
                CustomerId = customerId,
                RentItems = new List<RentalItem>
                {
                },
            };

            Order order2 = new()
            {
                Id = orderId2,
                CustomerId = customerId,
                RentItems = new List<RentalItem>
                {
                },
            };

            await unitOfWork.Orders.CreateOrderAsync(order1);
            await unitOfWork.Orders.CreateOrderAsync(order2);
            
            //Act
            var result = await unitOfWork.Orders.GetAllByCustomerAsync(customerId);

            Assert.Equal(result.Count(), expected);
        }
    }
}
