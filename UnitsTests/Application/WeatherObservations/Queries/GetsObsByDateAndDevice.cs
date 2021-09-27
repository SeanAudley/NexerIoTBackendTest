
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
using NexerTest.Application.WeatherObservations.Queries.GetObservationsByDate;
using NexerTest.Application.WeatherObservations.Queries.GetObservationsByDateDeviceCommand;

namespace NexerTestTests.Application.UnitTests.WeatherObservations.Queries
{


    public class GetObservationsByDateDeviceTest
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
        public async void ValidDate_RetrieveList()
        {
            GetObservationsByDateDeviceCommand test = new GetObservationsByDateDeviceCommand();

            var handler = new GetObservationsByDateDeviceCommand.GetObservationsByDateDeviceHandler();
            var query = new GetObservationsByDateDeviceCommand {DeviceName="Alpha", DateSelected= "26/09/2021 15:50" };
            var result = await handler.Handle(query, CancellationToken.None);

            result.Success.ShouldBe(true);
        }


        [Fact]
        public async void InvalidDate()
        {
            GetObservationsByDateDeviceCommand test = new GetObservationsByDateDeviceCommand();
            var handler = new GetObservationsByDateDeviceCommand.GetObservationsByDateDeviceHandler();
            var query = new GetObservationsByDateDeviceCommand {DeviceName="Alpha", DateSelected = "II/09/2021 15:50" };
            var result = await handler.Handle(query, CancellationToken.None);

            result.Success.ShouldBe(false);
        }

        [Fact]
        public async void NoDateEntered()
        {
            GetObservationsByDateDeviceCommand test = new GetObservationsByDateDeviceCommand();
            var handler = new GetObservationsByDateDeviceCommand.GetObservationsByDateDeviceHandler();
            var query = new GetObservationsByDateDeviceCommand { DateSelected = "" };
            var result = await handler.Handle(query, CancellationToken.None);

            result.Success.ShouldBe(false);
        }

        [Fact]
        public async void NoDeviceEntered()
        {
            GetObservationsByDateDeviceCommand test = new GetObservationsByDateDeviceCommand();
            var handler = new GetObservationsByDateDeviceCommand.GetObservationsByDateDeviceHandler();
            var query = new GetObservationsByDateDeviceCommand { DeviceName = "", DateSelected = "26/09/2021 15:50" };
            var result = await handler.Handle(query, CancellationToken.None);

            result.Success.ShouldBe(false);
        }
    }


}



