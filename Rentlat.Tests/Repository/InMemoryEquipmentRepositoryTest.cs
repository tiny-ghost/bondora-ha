using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Rental.Persistence;
using System;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Rentlat.Tests
{
    public class InMemoryEquipmentRepositoryTest : RepositoryTest, IDisposable
    {
        private readonly DbConnection _connection;

        public InMemoryEquipmentRepositoryTest() : base(new DbContextOptionsBuilder<RentalContext>()
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
        public async Task GetAllAsync_WhenCalled_ReturnAllEquipment()
        {
            //Arrange
            using var context = new RentalContext(ContextOptions);
            var unitOfWork = new UnitOfWork(context);
            var totalUnits = 5;

            //Act
            var result = await unitOfWork.Equipment.GetAllAsync();

            //Assert
            Assert.Equal(result.Count(), totalUnits);


        }

        [Fact]
        public async Task GetByIdAsync_WithGivenId_Should_ReturnCorrectEquipment()
        {
            //Arrange
            using var context = new RentalContext(ContextOptions);
            var unitOfWork = new UnitOfWork(context);
            var equipmentId = 3;

            //Act
            var result = await unitOfWork.Equipment.GetByIdAsync(equipmentId);

            //Assert
            Assert.Equal(result.Id, equipmentId);
        }
    }
}
