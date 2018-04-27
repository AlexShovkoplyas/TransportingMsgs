using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;
using System;
using System.Text;

namespace Receiver
{
    class Receiver
    {
        ConnectionFactory factory;
        private IModel channel;
        private int msgCounter = 1;

        private const string EXCH_NAME = "task_exch";
        private const string HOST_NAME = "localhost";

        public Receiver(string userName)
        {
            QueueName = userName + "_queue";
            factory = new ConnectionFactory() { HostName = "localhost" };
        }

        public string QueueName { get; }

        public void Listening()
        {
            using (var connection = factory.CreateConnection())
            {
                using (channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(
                        exchange: EXCH_NAME,
                        type: "direct");

                    channel.QueueDeclare(
                        queue: QueueName,
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);


                    channel.QueueBind(
                        queue: QueueName,
                        exchange: EXCH_NAME,
                        routingKey: "");

                    if (false)
                    {
                        //var c2 = new DefaultBasicConsumer(channel);
                        //c2.HandleBasicConsumeOk()

                        //var consumerInitial = new QueueingBasicConsumer(channel);
                        //int counter = 0;
                        //while (counter < msgCount) 
                        //{
                        //    ReceiveMessage(null, consumerInitial.Queue.Dequeue());
                        //    counter++;
                        //} 
                    }

                    if (false)
                    {
                        Subscription sub = new Subscription(channel, QueueName, false);

                        foreach (BasicDeliverEventArgs e in sub)
                        {
                            ReceiveMessage(null, e);
                        }
                    }

                    if (true)
                    {
                        BasicGetResult result;
                        do
                        {
                            result = channel.BasicGet(QueueName, false);
                            if (result != null)
                            {
                                ReceiveMessage(result);
                            }
                        } while (result != null);

                        var consumer = new EventingBasicConsumer(channel);
                        consumer.Received += ReceiveMessage;

                        channel.BasicConsume(
                            queue: QueueName,
                            consumer: consumer,
                            autoAck: false);
                    }


                    Console.WriteLine("Press \"q\" to Exit");
                    string input;
                    do
                    {
                        input = Console.ReadLine();
                    } while (input.ToLower() != "q");
                }
            }
        }

        private void ReceiveMessage(BasicGetResult e)
        {
            var body = e.Body;
            var message = Encoding.Default.GetString(body);
            Console.WriteLine($"{msgCounter++}: Message received - {message}");
            channel.BasicAck(e.DeliveryTag, false);
        }

        private void ReceiveMessage(object sender, BasicDeliverEventArgs e)
        {
            var body = e.Body;
            var message = Encoding.Default.GetString(body);
            Console.WriteLine($"{msgCounter++}: Message received - {message}");
            channel.BasicAck(e.DeliveryTag, false);
        }
    }
}
