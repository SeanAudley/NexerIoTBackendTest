using System;
using NexerTest.Development;

namespace NexerTest.Development{
    public class DateTimeService : IDateTimeService {
        public DateTime Now { get { return DateTime.Now; } }
    }
}

