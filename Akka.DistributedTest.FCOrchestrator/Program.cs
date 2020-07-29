using Akka.Actor;
using System;
using System.Threading.Tasks;

namespace Akka.DistributedTest.FCOrchestrator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using(var sys = ActorSystem.Create("order-processor"))
            {

                //the guardian is the root node of the actor tree. If he dies, the entire actor system dies, ensure he has no risky logic that can fail
                //he will receive a message from another computer once he gets the signal to start
                var guardian = sys.ActorOf(Props.Create<OrderIngestionActor>(), "order-ingestion-actor");
                Console.WriteLine("Order ingestion actor started");


                await sys.WhenTerminated;
            }
        }
    }
}
