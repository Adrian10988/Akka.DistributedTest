using Akka.Actor;
using Akka.DistributedTest.Models.OrderIngesetionActor;
using System;
using System.Threading.Tasks;

namespace Akka.DistributedTest.InventoryAllocation
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (var sys = ActorSystem.Create("inventory-allocation"))
            {
                var orderIngestionActor = sys.ActorSelection("akka.tcp://order-processor@localhost:8082/user/order-ingestion-actor");
                orderIngestionActor.Tell(new StartIngestion());

                var orderIngestionEventHandlers = sys.ActorOf(Props.Create<OrderIngestionEventHandler>());
                orderIngestionEventHandlers.Tell(new OrderIngestionEventHandler.SubscribeToOrderIngestionEvents());
                Console.WriteLine("Started trigger to ingestion");


                await sys.WhenTerminated;
            }
        }
    }
}
