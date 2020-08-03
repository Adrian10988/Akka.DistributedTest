using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using Akka.Cluster;
using Akka.Cluster.Sharding;
using Akka.Configuration;
using Akka.DistributedTest.Models;
using Akka.DistributedTest.Models.ShardTest;
using Petabridge.Cmd.Cluster;
using Petabridge.Cmd.Cluster.Sharding;
using Petabridge.Cmd.Host;
using Petabridge.Cmd.Remote;
using static Akka.Actor.Props;

namespace ShardTest
{
    public class OrderIngestionActor : ReceiveActor
    {
        private readonly Props _routerProps;
        private readonly IActorRef _region;
        private readonly Random _r;
        public OrderIngestionActor()
        {
            _r = new Random();

            //sharded

            var creatorProps = Create<FCInventoryAllocationActor>();

            var shardExtension = ClusterSharding.Get(Context.System);
            var shardSettings = ClusterShardingSettings.Create(Context.System);

            _region =
                shardExtension.Start("fc",
                    creatorProps,
                    shardSettings,
                    new MessageExtractor());

           var pbm = PetabridgeCmd.Get(Context.System);
           pbm.RegisterCommandPalette(ClusterCommands.Instance);
           pbm.RegisterCommandPalette(ClusterShardingCommands.Instance);
           pbm.RegisterCommandPalette(RemoteCommands.Instance);
           pbm.Start();

            Receive<SendMessage>(a =>
            {

                var fcId = GenerateRandomFCID();
                _region.Tell(new FCActorMessages.OrderIngested(fcId));

            });

            Receive<BeginTest>(a =>
            {
                Context.System.Scheduler.ScheduleTellRepeatedly(
                    TimeSpan.FromSeconds(0), TimeSpan.FromMilliseconds(500), Self, new SendMessage(), ActorRefs.NoSender);
            });

        }

        private int GenerateRandomFCID()
        {
            return _r.Next(1, 8);
        }

        public class BeginTest
        {

        }

        private class SendMessage
        {

        }

    }
}
