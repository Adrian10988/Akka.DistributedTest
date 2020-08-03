using Akka.Actor;
using Akka.DistributedTest.Models.OrderIngesetionActor;
using Akka.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akka.DistributedTest.InventoryAllocation
{
    public class OrderIngestionEventHandler : ReceiveActor
    {
        private readonly ActorSelection _responder;
        private readonly Props _consistentHashRouter;
        public OrderIngestionEventHandler()
        {
            _responder = Context.ActorSelection("akka.tcp://order-processor@localhost:8082/user/order-ingestion-actor");
            
            Receive<OrderIngested>(a =>
            {
                Console.WriteLine($"{a.FulfillmentCenterId} Received order {a.Id}");
                var actor = Context.ActorOf(_consistentHashRouter);
                actor.Tell(a);
            });

            Receive<SubscribeToOrderIngestionEvents>(a =>
            {
                _responder.Tell(new Subscribe(Self));
            });

            _consistentHashRouter = new ConsistentHashingPool(2)
                .WithHashMapping(a =>
                {
                    if (a is OrderIngested)
                    {
                        return ((OrderIngested)a).FulfillmentCenterId;
                    }

                    return null;
                }).Props(Props.Create<FCAllocationActor>());
        }

        public class SubscribeToOrderIngestionEvents
        {

        }
    }
}
