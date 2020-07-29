using Akka.Actor;
using Akka.DistributedTest.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akka.DistributedTest.ClientOne
{
    public class InitializationActor : ReceiveActor
    {
        private int _messagesReceived = 0;
        private readonly ActorSelection _responder = Context.ActorSelection("akka.tcp://sys@localhost:8080/user/responder");
        public InitializationActor()
        {
            Become(Ready);
        }

        private void Ready()
        {
            Receive<CallResponseActor>(a =>
            {
                Become(InAConversation);
                _responder.Tell(new Ping(_messagesReceived, "Answer me!"));
            });
        }

        private void InAConversation()
        {
            Receive<Ping>(a =>
            {
                _messagesReceived++;
                Console.WriteLine($"PONG {a.SenderReceivedCount}");
                Sender.Tell(new Ping(_messagesReceived, "What's up?"));
            });
            Receive<HangUp>(a =>
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(a.Message);
                Become(Ready); //ready to trigger a new conversation;
            });


        }


    }
}
