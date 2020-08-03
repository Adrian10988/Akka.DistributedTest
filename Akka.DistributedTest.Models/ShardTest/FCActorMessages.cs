using System;
using System.Collections.Generic;
using System.Text;

namespace Akka.DistributedTest.Models.ShardTest
{
    public class FCActorMessages
    {
        public class ProcessMessage
        {
            public readonly int FCID;

            public ProcessMessage(int fcid)
            {
                FCID = fcid;
            }
        }
    }
}
