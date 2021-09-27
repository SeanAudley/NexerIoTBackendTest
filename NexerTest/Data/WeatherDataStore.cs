using NexerTest.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NexerTest.Data;
using Microsoft.Azure;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using NexerTest.Data.ViewModels;

namespace NexerTest.Data
{
    public class WeatherDataStore
    {
        private static List<Weather> _observations;

        public WeatherDataStore()
        {
            _observations = new List<Weather>
            {
                new Weather {ObservationTime= DateTime.Now, Humidity=12, Rainfall=1, Temperature=17, DeviceName="Unit1"},
                new Weather {ObservationTime= DateTime.Now.AddMinutes(5), Humidity=15, Rainfall=1, Temperature=17,DeviceName="Unit2" },
                new Weather {ObservationTime= DateTime.Now.AddMinutes(10), Humidity=16, Rainfall=1, Temperature=16.9 ,DeviceName="Unit1" }
            };
           
        }
        public IEnumerable<Weather> GetAll() => _observations;

        public async Task<List<Weather>> RetrieveBlobByDate(DateTime dateSelected)
        {
            string dateConv = dateSelected.ToString().Replace("/", "");
            dateConv = dateConv.Replace("00:00:00", "").TrimEnd();

            BlobStore blobData = new BlobStore();
            string sourceBlobFileName = dateConv + ".csv";
            return await BlobStore.GetCSVBlobData(sourceBlobFileName) ;

        }
    }
}
