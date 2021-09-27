using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NexerTest.Data
{
    public class Weather
    {
        public DateTime ObservationTime { get; set; }
        public double Rainfall { get; set; }
        public double Humidity { get; set; }
        public double Temperature { get; set; }

        public string DeviceName { get; set; }
    
    }
}
