
using Azure.Storage.Blobs;
using CsvHelper;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.File;
using NexerTest.Data.ViewModels;
using NexerTest.Development;
using NexerTest.Development.NexerHttpClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static NexerTest.Development.ConfigurationHelper;
namespace NexerTest.Data
{
    public class BlobStore
    {
        public static async Task<List<Weather>> GetCSVBlobData(string filename)
        {
       
            string connectionString = ConfigurationHelper.GetByName("NexerTestConfig:StorageConnectionString");//blob connection string
            string ContainerName = ConfigurationHelper.GetByName("NexerTestConfig:ContainerName"); //source blob container name           


            BlobServiceClient storageAccount = new BlobServiceClient(connectionString);
            BlobContainerClient containerClient = storageAccount.GetBlobContainerClient(ContainerName);
            BlobClient blobClient = containerClient.GetBlobClient(filename);

            List<Weather> returnObs = new List<Weather>();
          
            try {
                
                string returnText = "";
                if (blobClient.Exists())
                {
                    var response = blobClient.DownloadContentAsync();
                    
                    using (var streamReader = new StreamReader(response.Result.Value.Content.ToStream()))
                    {
                        bool ignoreHeaders = true;
                        while (!streamReader.EndOfStream)
                        {
                            var line = await streamReader.ReadLineAsync();
                            if (!ignoreHeaders)
                            {
                                string[] temp = line.Split(',');
                                if (temp.Length >= 2)
                                {
                                    Weather itm = new Weather()
                                    {
                                        ObservationTime = DateTime.Parse(temp[0].Trim()),
                                        Humidity = double.Parse(temp[1].Trim()),
                                        Rainfall = double.Parse(temp[2].Trim()),
                                        Temperature = double.Parse(temp[3].Trim()),
                                        DeviceName = temp[4].Trim()
                                    };
                                    returnObs.Add(itm);
                                    //returnText = returnText + line;
                                }
                            }
                            else
                            { ignoreHeaders = false; }
                        }
                    }
                }
                return returnObs.ToList() ;
            } 
            catch (Exception e)
            {
                return null;
            }
        }
    }
}