using System;
using System.Collections.Generic;
using System.Text;

namespace Akka.DistributedTest.Models
{
    public class Ping
    {
        public readonly int SenderReceivedCount;
        public readonly string Message;

        public Ping(int senderReceivedCount, string message)
        {
            SenderReceivedCount = senderReceivedCount;
            Message = message;
        }
    }
}
