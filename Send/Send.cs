using System;
using System.Text;
using RabbitMQ.Client;

class Send
{
    public static void Main()
    {
        // Create a connection factory
        var factory = new ConnectionFactory { HostName = "localhost" };

        // Establish a connection
        using var connection = factory.CreateConnection();

        // Create a channel
        using var channel = connection.CreateModel();

        // Declare a queue
        channel.QueueDeclare(queue: "hello",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        Console.WriteLine("Enter a message to send. Type 'exit' to quit.");

        while (true)
        {
            // Read input from the console
            string? message = Console.ReadLine();

            // Check if the user wants to exit
            if (message == null || message.ToLower() == "exit")
                break;

            // Convert the message to a byte array
            var body = Encoding.UTF8.GetBytes(message);

            // Publish the message to the queue
            channel.BasicPublish(exchange: string.Empty,
                                 routingKey: "hello",
                                 basicProperties: null,
                                 body: body);
            Console.WriteLine($" [x] Sent {message}");
        }

        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();
    }
}
