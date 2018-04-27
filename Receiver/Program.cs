using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Receiver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your name: ");
            var userName = Console.ReadLine();

            var receiver = new Receiver(userName);
            receiver.Listening();
        }
    }
}
