using NexerTest.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NexerTest.Data
{
    public class WeatherDataStore
    {
        private static List<Weather> _observations;
        public WeatherDataStore()
        {
        _observations = new List<Weather>
        {
            new Weather {ObservationTime= DateTime.Now, Humidity=12, Rainfall=1, Temperature=17},
            new Weather {ObservationTime= DateTime.Now.AddMinutes(5), Humidity=15, Rainfall=1, Temperature=17 },
            new Weather {ObservationTime= DateTime.Now.AddMinutes(10), Humidity=16, Rainfall=1, Temperature=16.9  }
        };
        }
        public IEnumerable<Weather> GetAll() => _observations;
    }
}
