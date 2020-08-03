using System;
using System.Collections.Generic;
using System.Text;

namespace Akka.DistributedTest.Models.ShardTest
{
    public class FCActorMessages
    {
        public class OrderIngested
        {
            public readonly int FCID;
            public readonly Guid OrderId;
            public OrderIngested(int fcid)
            {
                FCID = fcid;
                OrderId = Guid.NewGuid();
            }
        }
    }
}
