using Confluent.Kafka;

var configProducer = new ProducerConfig
{
    BootstrapServers = "localhost:9092"
};

using var producer = new ProducerBuilder<Null, string>(configProducer).Build();
var topic = "test-topic";

Console.WriteLine("Enter messages to send to Kafka (type 'exit' to quit):");

while (true)
{
    var userInput = Console.ReadLine();
    if (userInput.ToLower() == "exit")
        break;

    var message = new Message<Null, string> { Value = userInput };

    producer.Produce(topic, message, deliveryReport => {
        if (deliveryReport.Error.IsError)
        {
            Console.WriteLine($"Error: {deliveryReport.Error.Reason}");
        }
        else
        {
            Console.WriteLine($"Message sent: {deliveryReport.Message.Value}");
        }
    });

    producer.Flush(TimeSpan.FromSeconds(10));
}