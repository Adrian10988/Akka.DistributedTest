using Akka.Actor;
using Akka.DistributedTest.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akka.DistributedTest.ClientTwo
{
    public class ResponseActor : ReceiveActor
    {
        private int _messagesReceived = 0;
        public ResponseActor()
        {
            Become(Ready);
        }

        private void Ready()
        {
            Receive<Ping>(a =>
            {
                _messagesReceived++;
                Console.WriteLine($"PING {a.SenderReceivedCount}");
                if (_messagesReceived < 100)
                    Sender.Tell(new Ping(_messagesReceived, "Hello!"));
                else
                {
                    Sender.Tell(new HangUp( "Ok nice talking to you. Gotta go now!"));
                    Context.System.Stop(Self); //will terminate the entire actor system because it's the only actor in the tree
                }
                    
            });
        }
    }
}
