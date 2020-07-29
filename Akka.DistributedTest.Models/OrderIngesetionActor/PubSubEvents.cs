using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akka.DistributedTest.Models.OrderIngesetionActor
{
    public class Subscribe
    {
        public readonly IActorRef Subscriber;
        public Subscribe(IActorRef subscriber)
        {
            Subscriber = subscriber;
        }
    }

    public class Unsubscribe : Subscribe
    {
        public Unsubscribe(IActorRef subscriber) : base(subscriber) { }
    }
}
