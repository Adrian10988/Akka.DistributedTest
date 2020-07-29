using Akka.Actor;
using System;
using System.Threading.Tasks;

namespace Akka.DistributedTest.ClientTwo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (var sys = ActorSystem.Create("sys")) //should pull app.config file in automatically
            {
                var initializationActor = sys.ActorOf(Props.Create<ResponseActor>(), "responder");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Actor system two is up");
                Console.ResetColor();
                await sys.WhenTerminated;
            }
        }
    }
}
