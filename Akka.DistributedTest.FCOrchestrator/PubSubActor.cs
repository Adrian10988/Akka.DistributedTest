using Akka.Actor;
using Akka.DistributedTest.Models.OrderIngesetionActor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Akka.DistributedTest.FCOrchestrator
{
    public class PubSubActor : ReceiveActor
    {
        private Dictionary<string, IActorRef> _subscriptions = new Dictionary<string, IActorRef>();
        public PubSubActor()
        {
            Receive<Subscribe>(a =>
            {
                if (!_subscriptions.ContainsKey(a.Subscriber.Path.Address.ToString()))
                {
                    _subscriptions.Add(a.Subscriber.Path.Address.ToString(), a.Subscriber);
                }
            });

            Receive<Unsubscribe>(a =>
            {
                if (_subscriptions.ContainsKey(a.Subscriber.Path.Address.ToString()))
                {
                    _subscriptions.Remove(a.Subscriber.Path.Address.ToString());
                }
            });

            Receive<OrderIngested>(a =>
            {
                if (_subscriptions.Any())
                {
                    foreach(var kvp in _subscriptions)
                    {
                        //forward on behalf of the originating actor
                        kvp.Value.Forward(a);
                    }
                }
            });

        }
    }
}
