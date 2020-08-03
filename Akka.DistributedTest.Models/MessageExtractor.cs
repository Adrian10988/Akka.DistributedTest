using System;
using System.Collections.Generic;
using System.Text;
using Akka.Cluster.Sharding;
using Akka.DistributedTest.Models.ShardTest;

namespace Akka.DistributedTest.Models
{
    public class MessageExtractor : IMessageExtractor
    {
        public string EntityId(object message)
        {
            return (message as FCActorMessages.OrderIngested)?.FCID.ToString();
        }

        public object EntityMessage(object message)
        {
            return message as FCActorMessages.OrderIngested;
        }

        public string ShardId(object message)
        {
            var hash = (message as FCActorMessages.OrderIngested)?.FCID.GetHashCode();
            var shardId = hash % 100;
            return shardId.ToString();
        }
    }
}
