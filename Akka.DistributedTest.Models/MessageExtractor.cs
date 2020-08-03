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
            return (message as FCActorMessages.ProcessMessage)?.FCID.ToString();
        }

        public object EntityMessage(object message)
        {
            return message as FCActorMessages.ProcessMessage;
        }

        public string ShardId(object message)
        {
            var hash = (message as FCActorMessages.ProcessMessage)?.FCID.GetHashCode();
            var shardId = hash % 100;
            return shardId.ToString();
        }
    }
}
