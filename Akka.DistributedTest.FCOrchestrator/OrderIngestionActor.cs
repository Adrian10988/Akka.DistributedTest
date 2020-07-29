using Akka.Actor;
using Akka.DistributedTest.Models.OrderIngesetionActor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akka.DistributedTest.FCOrchestrator
{
    public class OrderIngestionActor : ReceiveActor
    {
        private ICancelable _cancelable;
        private readonly IActorRef _pubSubActor;
        private readonly Random _r;
        public OrderIngestionActor()
        {
            _r = new Random();
            _pubSubActor = Context.ActorOf(Props.Create<PubSubActor>(), "pub-sub-actor");
            Become(Standby);
        }

        private void Standby()
        {
            Receive<StartIngestion>(a =>
            {
                Become(Running);
                //we use a proxy event here to activate an event handler from within this class
                //to create a new fake order instead of just passing a fake order because the scheduler will just schedule
                //the same order over and over
                _cancelable = Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(0,
                    500, Self, new CreateNewFakeOrder(), ActorRefs.Nobody);
            });
        }

        private OrderIngested CreateNewOrder()
        {
            return new OrderIngested(Guid.NewGuid(), _r.Next(1, 8));
        }

        private void Running()
        {
            Receive<StopIngestion>(a =>
            {
                _cancelable.Cancel();
                Become(Standby);
            });

            Receive<OrderIngested>(a =>
            {
                Console.WriteLine($"{a.FulfillmentCenterId} ingested order {a.Id}");
                _pubSubActor.Tell(a);
            });

            Receive<Subscribe>(a =>
            {
                _pubSubActor.Tell(a);
            });

            Receive<CreateNewFakeOrder>(a =>
            {
                Self.Tell(CreateNewOrder());
            });
        }

        private class CreateNewFakeOrder
        {

        }

    }
}
