using MediatR;
using NexerTest.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NexerTest.Data.ViewModels;
using System.Threading;
using System;
using Newtonsoft.Json;
using NexerTest.Development.NexerHttpClient;

namespace NexerTest.Application.GetObservationsQuery.Queries
{
    public class GetObservationsCommand: IRequest<Result>
    {

        public class GetObservationsHandler : IRequestHandler<GetObservationsCommand, Result>
        {
            private WeatherDataStore _data = new WeatherDataStore();
            private readonly INexerHttpClient _client;
            private readonly IMediator _mediator;

            public GetObservationsHandler()//INexerHttpClient client ,IMediator mediator)
            {
              //  _client = client;
               // _mediator = mediator;
            }

            public async Task<Result> Handle(GetObservationsCommand request, CancellationToken cancellationToken)
            {
             
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