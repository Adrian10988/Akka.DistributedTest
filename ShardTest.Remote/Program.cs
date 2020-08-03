using System;
using Akka.Actor;
using Akka.Cluster.Sharding;
using Akka.DistributedTest.Models;

namespace ShardTest.Remote
{
    class Program
    {
        private static IActorRef _region;
        static void Main(string[] args)
        {
            using var sys = ActorSystem.Create("sys");

            //sharded

            var creatorProps = Props.Create<FCActor>();

            var shardExtension = ClusterSharding.Get(sys);
            var shardSettings = ClusterShardingSettings.Create(sys);

            _region =
                shardExtension.Start("fc",
                    creatorProps,
                    shardSettings,
                    new MessageExtractor());

            sys.WhenTerminated.Wait();
        }
    }
}
