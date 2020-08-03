using System;
using Akka.Actor;
using Akka.Cluster;
using Akka.Cluster.Routing;
using Akka.Cluster.Sharding;
using Akka.Routing;

namespace ShardTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using var sys = ActorSystem.Create("sys");

            //var router = new ClusterRouterPoolSettings(6, 2, true);
            //var fcProps =
            //    Props.Create<FCActor>().WithRouter(new ClusterRouterPool(
            //        new ConsistentHashingPool(8)
            //            .WithHashMapping(a =>
            //            {
            //                return (a as FCActor.ProcessMessage)?.FCID.ToString();
            //            }), router));

            var creator = sys.ActorOf(Props.Create<MessageCreator>());
            creator.Tell(new MessageCreator.BeginTest());

            sys.WhenTerminated.Wait();
        }
    }
}
