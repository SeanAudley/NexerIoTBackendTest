
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
using NexerTest.Application.UnitTests.Common;
using System.Net;

namespace NexerTestTests.Application.UnitTests.WeatherObservations.Queries
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
        public async void RetrieveList()
        {
            GetObservationsCommand test = new GetObservationsCommand();
            
            var handler = new GetObservationsCommand.GetObservationsHandler();
            var query = new GetObservationsCommand { };
            var result = await handler.Handle(query, CancellationToken.None);
            
            result.Success.ShouldBe(true);
        }




    }

    
}



