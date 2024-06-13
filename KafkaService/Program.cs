using Confluent.Kafka;

var configConsumer = new ConsumerConfig
{
    BootstrapServers = "localhost:9092",
    GroupId = "test-group",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

using var consumer = new ConsumerBuilder<Ignore, string>(configConsumer).Build();
consumer.Subscribe("test-topic");
try
{
    while (true)
    {
        var message1 = consumer.Consume();
        Console.WriteLine($"Received message: {message1.Value}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}