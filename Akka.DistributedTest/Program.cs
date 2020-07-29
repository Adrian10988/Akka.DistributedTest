using Akka.Actor;
using Akka.DistributedTest.ClientOne;
using System;
using System.Threading.Tasks;

namespace Akka.DistributedTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (var sys = ActorSystem.Create("sys")) //should pull app.config file in automatically
            {
                var initializationActor = sys.ActorOf(Props.Create<InitializationActor>());

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Actor system one is up. Running to get the telephone...");
                Console.ResetColor();

                await Task.Delay(2000);
                //dial phone number
                initializationActor.Tell(new CallResponseActor());

                await sys.WhenTerminated;
            }
        }
    }
}
