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

namespace NexerTest.Application.WeatherObservations.Queries.GetObservationsByDateDeviceCommand
{
    public class GetObservationsByDateDeviceCommand : IRequest<Result>
    {

        public string DateSelected { get; set; }
        public string DeviceName { get; set; }


        public class GetObservationsByDateDeviceHandler : IRequestHandler<GetObservationsByDateDeviceCommand, Result>
        {
            private WeatherDataStore _data = new WeatherDataStore();
            private readonly INexerHttpClient _client;
            private readonly IMediator _mediator;

            public GetObservationsByDateDeviceHandler()//INexerHttpClient client ,IMediator mediator)
            {
                //  _client = client;
                // _mediator = mediator;
            }

            public async Task<Result> Handle(GetObservationsByDateDeviceCommand request, CancellationToken cancellationToken)
            {
                if (string.IsNullOrEmpty(request.DateSelected.ToString()))
                {
                    return new Result { Payload = "No DateSelected was given", Exception = new Exception("Error -> No DateSelected") };
                }
                if (string.IsNullOrEmpty(request.DeviceName.ToString()))
                {
                    return new Result { Payload = "No DeviceName was entered", Exception = new Exception("Error -> No DeviceName Entered") };
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
                    //data.Where(c=> string.Equals(c.DeviceName.ToUpper().Trim(), request.DeviceName.ToUpper().Trim(), StringComparison.OrdinalIgnoreCase));
                    var query = from c in data where c.DeviceName.Equals(request.DeviceName) select c;
                    return new Result
                    {
                        Payload = JsonConvert.SerializeObject(query)
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