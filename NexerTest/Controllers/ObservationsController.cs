using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NexerTest.Application.GetObservationsQuery.Queries;
using NexerTest.Application.WeatherObservations.Queries.GetObservationsByDate;

namespace NexerTest.Controllers
{
    [Route("api/observations")]
    [ApiController]
    public class ObservationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ObservationsController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult> GetObservations()
        {
            var weather= await _mediator.Send(new GetObservationsCommand());
            return Ok(weather);
        }

        [HttpGet("[action]/{dateTimeSelected}")]
        public async Task<ActionResult> GetObservationsByDate(string dateTimeSelected)
        {
            var weather = await _mediator.Send(new GetObservationsByDateCommand { DateSelected = dateTimeSelected } );
            return Ok(weather);
        }
    }
} 
