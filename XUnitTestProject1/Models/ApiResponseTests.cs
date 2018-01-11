using System;
using CoinArbiter.Models;
using Moq;
using Xunit;

namespace XUnitTestProject1.Models
{
    public class ApiResponseTests : IDisposable
    {
        private MockRepository mockRepository;



        public ApiResponseTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
        }

        public void Dispose()
        {
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void FullCoinGetSetDatum()
        {
            // Arrange
            ApiResponse apiResponse = this.CreateApiResponse();
            Datum[] theData = new Datum[2];
            apiResponse.Data = theData;
            Datum testCoin = new Datum();
            testCoin.Close = 5.00;

            // Act
            for (int i = 0; i < apiResponse.Data.Length; i++)
            {
                apiResponse.Data[i] = testCoin;
            }

            // Assert
            Assert.Equal(testCoin, apiResponse.Data[0]);
            Assert.Equal(testCoin, apiResponse.Data[1]);
        }

        private ApiResponse CreateApiResponse()
        {
            return new ApiResponse();
        }
    }
}
