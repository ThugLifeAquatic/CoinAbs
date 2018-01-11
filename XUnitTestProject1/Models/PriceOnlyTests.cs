using CoinArbiter.Models;
using Moq;
using System;
using Xunit;

namespace XUnitTestProject1.Models
{
    public class PriceOnlyTests : IDisposable
    {
        private MockRepository mockRepository;



        public PriceOnlyTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);


        }

        public void Dispose()
        {
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void SetGetPrice()
        {
            // Arrange
            PriceOnly priceOnly = this.CreatePriceOnly();
            double price = 5.42;


            // Act
            priceOnly.Usd = price;


            // Assert
            Assert.Equal(priceOnly.Usd, price);

        }

        private PriceOnly CreatePriceOnly()
        {
            return new PriceOnly();
        }
    }
}
