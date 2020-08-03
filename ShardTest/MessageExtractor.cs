using System;
using System.Collections.Generic;
using System.Text;
using Akka.Cluster.Sharding;

namespace ShardTest
{
    public class MessageExtractor : IMessageExtractor
    {
        public string EntityId(object message)
        {
            return (message as FCActor.ProcessMessage)?.FCID.ToString();
        }

        public object EntityMessage(object message)
        {
            return message as FCActor.ProcessMessage;
        }

        public string ShardId(object message)
        {
            var hash = (message as FCActor.ProcessMessage)?.FCID.GetHashCode();
            var shardId = hash % 100;
            return shardId.ToString();
        }
    }
}
