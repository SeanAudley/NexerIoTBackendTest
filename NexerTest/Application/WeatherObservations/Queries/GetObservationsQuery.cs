using MediatR;
using NexerTest.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NexerTest.Data.ViewModels;
using System.Threading;
using System;
using Newtonsoft.Json;

namespace NexerTest.Application.GetObservationsQuery.Queries
{
    public class GetObservationsCommand: IRequest<Result>
    {

        public DateTime DateSelected { get; set; }



        public class GetObservationsHandler : IRequestHandler<GetObservationsCommand, Result>
        {
            private WeatherDataStore _data = new WeatherDataStore();

            private readonly IMediator _mediator;
            public GetObservationsHandler(IMediator mediator)
            {
                _mediator = mediator;
            }

            public async Task<Result> Handle(GetObservationsCommand request, CancellationToken cancellationToken)
            {
              /*  if (string.IsNullOrEmpty(request.DateSelected.ToString())) 
                {
                    return new Result {Payload= "No DateSelected was given", Exception = new Exception("Error -> No DateSelected") }; 
                }
                */
                
                try
                {

                    return new Result
                    {
                        Payload = JsonConvert.SerializeObject(_data.GetAll())
                    };
                }
                catch (Exception e)
                {
                    return new Result
                    {
                        Exception = e
                    };
                }


            }
        }
    }
}