using System;
using Akka.Actor;

namespace ShardTest.Remote
{
    class Program
    {
        static void Main(string[] args)
        {
            using var sys = ActorSystem.Create("sys");

            sys.WhenTerminated.Wait();
        }
    }
}
