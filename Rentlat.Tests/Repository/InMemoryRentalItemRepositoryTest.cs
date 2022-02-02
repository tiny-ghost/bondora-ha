using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Rental.Core.Models;
using Rental.Persistence;
using Rentlat.Tests;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Xunit;

namespace Rental.Tests.Repository
{
    public class InMemoryRentalItemRepositoryTest : RepositoryTest, IDisposable
    {
        private readonly DbConnection? _connection;

        public InMemoryRentalItemRepositoryTest() : base(new DbContextOptionsBuilder<RentalContext>()
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

        public void Dispose() => _connection?.Dispose();

        [Fact]
        public async Task AddAsync_WhenCalled_ShouldAddItemToDb()
        {
            //Arrange
            using var context = new RentalContext(ContextOptions);
            var unitOfWork = new UnitOfWork(context);

            var order = new Order()
            {
                CustomerId = 1,
                RentItems = new List<RentalItem> { },
            };

            await unitOfWork.Orders.AddAsync(order);

            var item = new RentalItem
            {
                DaysOfRental = 3,
                EquipmentId = 1,
                OrderId = 1,
            };

            //Act 
            var result = await unitOfWork.RentalItems.AddAsync(item);

            //Assert
            Assert.True(result is not null);

        }

        [Fact]
        public async Task AddAsync_WhenCalled_CantAddItemToDbWithoutExistingOrder()
        {
            //Arrange
            using var context = new RentalContext(ContextOptions);
            var unitOfWork = new UnitOfWork(context);
            var expected = "SQLite Error 19: 'FOREIGN KEY constraint failed'.";
            var result = "";
            var item = new RentalItem
            {
                DaysOfRental = 3,
                EquipmentId = 1,
                OrderId = 1,
            };

            //Act 
            try
            {
               await unitOfWork.RentalItems.AddAsync(item);
            }
            catch (Exception e)
            {

                result = e.InnerException?.Message;
            }


            //Assert
            Assert.Equal(expected, result);

        }
    }
   
}
