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

            //need to spin up an identical region found in shardtest project in order for 
            //akka to split messages between both nodes

            var creatorProps = Props.Create<FCInventoryAllocationActor>();

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
