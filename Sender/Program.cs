using RabbitMQ.Client;
using System;
using System.Text;

namespace Sender
{
    class Program
    {
        static void Main(string[] args)
        {
            var broadcaster = new Broadcaster();
            broadcaster.Broadcasting();
        }
    }
}
