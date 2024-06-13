using Confluent.Kafka;

var kafkaUrl = Environment.GetEnvironmentVariable("KAFKA_URL");

if (kafkaUrl == null)
{
    throw new ArgumentNullException("KAFKA_URL");
}

var configConsumer = new ConsumerConfig
{
    BootstrapServers = kafkaUrl,
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