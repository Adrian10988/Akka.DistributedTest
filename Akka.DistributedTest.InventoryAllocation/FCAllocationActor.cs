using Akka.Actor;
using Akka.DistributedTest.Models.OrderIngesetionActor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akka.DistributedTest.InventoryAllocation
{
    public class FCAllocationActor : ReceiveActor
    {
        public FCAllocationActor()
        {
            Receive<OrderIngested>(a =>
            {
                Console.WriteLine($"{Self.Path.Name} -- {a.FulfillmentCenterId}");
            });
        }
    }
}
