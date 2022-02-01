using Xunit;
using Moq;
using Rental.Core.Contracts.Service;

namespace Rentlat.Tests
{
    public class InvoiceServiceTest
    {
        private readonly IMock<IInvoiceService> _invoiceService;
        public InvoiceServiceTest()
        {
            _invoiceService = new Mock<IInvoiceService>();
        }

        [Fact]
        public void Te3()
        {

            //Arrange

            //Act

            //Assert
            Assert.True(true);


        }
    }
}