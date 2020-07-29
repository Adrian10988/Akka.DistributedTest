using System;
using System.Collections.Generic;
using System.Text;

namespace Akka.DistributedTest.Models
{
    public class HangUp 
    {
        public readonly string Message;
        public HangUp(string message)
        {
            Message = message;
        }
    }
}
