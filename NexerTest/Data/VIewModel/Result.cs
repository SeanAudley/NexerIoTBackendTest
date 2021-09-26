using System;
using NexerTest.Development;

namespace NexerTest.Data.ViewModels
{

    public class Result : IResult
    {

        public bool Success { get { return Exception == null; } }
        public object Payload { get; set; }
        public int RecordsChanged { get; set; }
        public Exception Exception { get; set; }

    }
}
