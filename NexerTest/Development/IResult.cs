using System;

namespace NexerTest.Development
{

    public interface IResult
    {
        bool Success { get; }
        object Payload { get; set; }
        int RecordsChanged { get; set; }
        Exception Exception { get; set; }
    }

}
