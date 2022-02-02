using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Moq;
using Rental.Core.Contracts.Service;
using Rental.Core.Models;
using Rental.Persistence;
using Rental.Service;
using Xunit;

namespace Rentlat.Tests
{
    public sealed class InvoiceServiceTest : RepositoryTest, IDisposable
    {
        private readonly DbConnection? _connection;
        public InvoiceServiceTest() : base(new DbContextOptionsBuilder<RentalContext>()
                  .UseSqlite(CreateInMemoryDatabase()).Options)
        {

            _connection = RelationalOptionsExtension.Extract(ContextOptions).Connection;
        }

        public void Dispose() => _connection?.Dispose();

        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();
            return connection;
        }

        [Fact]
        public async Task CreateTextInvoiceFromOrderAsync_WhenCalled_ShouldReturnNonEmptyMemoryStream()
        {

            //Arrange
            var expected = 0;
            using var context = new RentalContext(ContextOptions);
            var unitOfWork = new UnitOfWork(context);
            var invoiceService = new InvoiceService(unitOfWork);
            var orderService = new OrderService(unitOfWork);


            var order = new Order
            {
                CustomerId = 1,
                RentItems = new List<RentalItem>
                {
                    new RentalItem()
                    {
                        DaysOfRental = 3,
                        EquipmentId = 2,
                    },
                    new RentalItem()
                    {
                        DaysOfRental = 12,
                        EquipmentId = 4,
                    }
                }
            };

            await orderService.PlaceOrderForCustomerASync(order);

            //Act
            var stream = await invoiceService.CreateTextInvoiceFromOrderAsync(1);
            var result = stream.ToArray().Length;

            //Assert
            Assert.NotEqual(result, expected);


        }
    }
}