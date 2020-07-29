using System;
using System.Collections.Generic;
using System.Text;

namespace Akka.DistributedTest.Models.OrderIngesetionActor
{
    public class OrderIngested
    {
        public int FulfillmentCenterId { get; set; }
        public Guid Id { get; set; }
        public OrderIngested(Guid orderId, int fcId)
        {
            Id = orderId;
            FulfillmentCenterId = fcId;
        }
    }
    public class StartIngestion
    {

    }

    public class StopIngestion
    {

    }
}
