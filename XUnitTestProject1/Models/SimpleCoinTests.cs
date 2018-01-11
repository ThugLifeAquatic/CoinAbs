using CoinArbiter.Models;
using Moq;
using System;
using Xunit;

namespace XUnitTestProject1.Models
{
    public class SimpleCoinTests : IDisposable
    {
        private MockRepository mockRepository;



        public SimpleCoinTests()
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
            SimpleCoin simpleCoin = this.CreateSimpleCoin();
            double price = 6.54;

            // Act
            simpleCoin.USD = price;

            // Assert
            Assert.Equal(simpleCoin.USD, price);
        }

        [Fact]
        public void SetGetCurrency()
        {
            // Arrange
            SimpleCoin simpleCoin = this.CreateSimpleCoin();
            string currency = "EUR";

            // Act
            simpleCoin.Currency = currency;

            // Assert
            Assert.Equal(currency, simpleCoin.Currency);
        }

        private SimpleCoin CreateSimpleCoin()
        {
            return new SimpleCoin();
        }
    }
}
