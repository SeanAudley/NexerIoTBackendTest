using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NexerTest.Development
{
    public interface ISettings
    {
        string StorageConnectionString { get; set; }
        string ContainerName { get; set; }
    }
    public class Settings: ISettings
    {
        public string StorageConnectionString { get; set; }
        public string ContainerName { get; set; }
    }
    
}
