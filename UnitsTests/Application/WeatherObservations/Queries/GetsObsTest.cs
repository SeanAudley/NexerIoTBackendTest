using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks.Dataflow;
using System;
using System.Threading;
using System.Threading.Tasks;
using Shouldly;
using Xunit;
using Newtonsoft.Json;
using Moq;
using MediatR;
using NexerTest.Data.ViewModels;
using NexerTest.Application.GetObservationsQuery.Queries;

namespace NexerTestTests.WeatherObservations.Queries
{
    public class GetObservations 
    {
        private static Mock<IMediator> CreateMockIMediator<T>(Result response)
        {
            var mockMediator = new Mock<IMediator>();

            mockMediator
                .Setup(
                    m => m.Send(
                          It.IsAny<T>()
                        , It.IsAny<CancellationToken>())
                )
                .ReturnsAsync(response)
                .Verifiable(" was not sent.");

            return mockMediator;

        }

        [Fact]
        public async Task ShouldBeExceptionNoNHSNumber()
        {

            var client = MockHttpClientGet("api/products");
            var mockMediator = CreateMockIMediator<GetObservations>(new Result { Payload = "" });

            var query = new GetObservations { };

            var handler = new GetObservations.GetObservationsHandler(mockMediator.Object);

            var result = await handler.Handle(query, CancellationToken.None);

            result.Exception.ShouldBeOfType<ArgumentException>();
        }

       

     
    }
}



