using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sender
{
    class Broadcaster
    {
        private ConnectionFactory factory;
        private IModel channel;

        private const string EXCH_NAME = "task_exch";
        private const string HOST_NAME = "localhost";

        public Broadcaster()
        {
            factory = new ConnectionFactory() { HostName = HOST_NAME };
        }

        public void Broadcasting()
        {
            string input;

            while (true)
            {
                Console.WriteLine("Enter your [Message] or \"q\" to Exit.");
                input = Console.ReadLine();
                if (input.ToLower() == "q")
                {
                    Console.WriteLine("GoodBye!");
                    break;
                }
                else
                {
                    SendMessage(input);
                }
            }
        }

        private void SendMessage(string message)
        {
            using (var connection = factory.CreateConnection())
            {
                using (channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(
                        exchange: EXCH_NAME,
                        type: "direct");

                    byte[] body = Encoding.Default.GetBytes(message);

                    var props = channel.CreateBasicProperties();
                    props.Persistent = true;

                    channel.BasicPublish(
                        exchange: EXCH_NAME,
                        body: body,
                        basicProperties: props,
                        routingKey: "");

                    Console.WriteLine("Message send - {0}", message);
                }
            }
        }


    }
}
