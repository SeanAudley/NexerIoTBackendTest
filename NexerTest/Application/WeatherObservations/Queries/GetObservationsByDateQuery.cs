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

namespace NexerTest.Application.WeatherObservations.Queries.GetObservationsByDate
{
    public class GetObservationsByDateCommand : IRequest<Result>
    {

        public string DateSelected { get; set; }



        public class GetObservationsByDateHandler : IRequestHandler<GetObservationsByDateCommand, Result>
        {
            private WeatherDataStore _data = new WeatherDataStore();
            private readonly INexerHttpClient _client;
            private readonly IMediator _mediator;

            public GetObservationsByDateHandler()//INexerHttpClient client ,IMediator mediator)
            {
                //  _client = client;
                // _mediator = mediator;
            }

            public async Task<Result> Handle(GetObservationsByDateCommand request, CancellationToken cancellationToken)
            {
                if (string.IsNullOrEmpty(request.DateSelected.ToString()))
                {
                    return new Result { Payload = "No DateSelected was given", Exception = new Exception("Error -> No DateSelected") };
                }
                
                DateTime dateTimeEnteredConverted;
                string replaceSlashData = "";
                if (request.DateSelected.Contains(""))
                {
                    replaceSlashData = request.DateSelected.Replace("%2F", "/");
                }
                else
                {
                    replaceSlashData = request.DateSelected;
                }
                
                if (DateTime.TryParse(replaceSlashData, out dateTimeEnteredConverted )== false)
                {
                    return new Result { Payload = "No Valid Date was given", Exception = new Exception("Error -> No Valid Date Entered") };
                }

                try
                {
                    List<Weather> data= await _data.RetrieveBlobByDate(dateTimeEnteredConverted);
                    data.Where(c=> c.ObservationTime==dateTimeEnteredConverted);
                    return new Result
                    {
                        Payload = JsonConvert.SerializeObject(data)
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