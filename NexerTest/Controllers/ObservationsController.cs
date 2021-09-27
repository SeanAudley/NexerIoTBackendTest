using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NexerTest.Application.GetObservationsQuery.Queries;
using NexerTest.Application.WeatherObservations.Queries.GetObservationsByDate;
using NexerTest.Application.WeatherObservations.Queries.GetObservationsByDateDeviceCommand;

namespace NexerTest.Controllers
{
    [Route("/api/v1/devices/")]
    [ApiController]
    public class ObservationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ObservationsController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// GetDevicesObservations simply returns the dummy data
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetDevicesObservations()
        {
            var weather= await _mediator.Send(new GetObservationsCommand());
            return Ok(weather);
        }
        
        /// <summary>
        /// This function retrieves the data for all devices from the Azure Storage blob by using the CSV file (e.g 26092021.csv, etc)
        /// </summary>
        /// <param name="dateTimeSelected"></param>
        /// <returns></returns>
        [HttpGet("/{dateTimeSelected}")]
        public async Task<ActionResult> GetDevicesObservationsByDate(string dateTimeSelected)
        {
            var weather = await _mediator.Send(new GetObservationsByDateCommand { DateSelected = dateTimeSelected } );
            return Ok(weather);
        }

        /// <summary>
        /// This function retrieves the data for a specific device on a specific date from the Azure Storage blob by using the CSV file (e.g 26092021.csv, etc)
        /// </summary>
        /// <param name="dateTimeSelected"></param>
        /// <returns></returns>
        [HttpGet("{deviceName}/{dateTimeSelected}")]
        public async Task<ActionResult> GetDeviceObservationsByDateAndDevice(string deviceName, string dateTimeSelected)
        {
            var weather = await _mediator.Send(new GetObservationsByDateDeviceCommand {DeviceName=deviceName, DateSelected = dateTimeSelected });
            return Ok(weather);
        }
    }
} 
