using System;
using RabbitMQ.Client;
using System.Text;

class Send
{
    public static void Main()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };

        using (var connection = factory.CreateConnection())
        {
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: "hello",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                string message = "";

                do
                {
                    Console.WriteLine(" Type your message.");
                    message = Console.ReadLine();

                } while (string.IsNullOrEmpty(message));


                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "", routingKey: "hello", basicProperties: null, body: body);

                Console.WriteLine(" [X] Sent {0}", message);

            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }

}