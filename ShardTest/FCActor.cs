using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using Akka.Cluster;
using Akka.Cluster.Sharding;
using Akka.Configuration;
using static Akka.Actor.Props;
using static Akka.DistributedTest.Models.ShardTest.FCActorMessages;

namespace ShardTest
{
    public class FCActor : ReceiveActor
    {

        public FCActor()
        {
            Receive<ProcessMessage>(a =>
            {
                var cluster = Cluster.Get(Context.System);
                Console.WriteLine($"Actor {Context.Self.Path.Name} -- {cluster.SelfAddress} -- {a.FCID}");
            });
        }

    }
}
