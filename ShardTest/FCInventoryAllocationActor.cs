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
    public class FCInventoryAllocationActor : ReceiveActor
    {

        public FCInventoryAllocationActor()
        {
            Receive<OrderIngested>(a =>
            {
                var cluster = Cluster.Get(Context.System);
                Console.WriteLine($"Node {cluster.SelfAddress} ::: Fulfillment Center {a.FCID} ::: Allocating inventory for order {a.OrderId}");
            });
        }

    }
}
