using Amazon;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;

class Program
{
    static async Task Main(string[] args)
    {
        var snsClient = new AmazonSimpleNotificationServiceClient(RegionEndpoint.USEast2);
        var topicArn = "YOUR_TOPIC_ARN";
        var message = "Hello, this is a message from the publisher.";

        var request = new PublishRequest(topicArn, message);

        var response = await snsClient.PublishAsync(request);

        Console.WriteLine($"Message sent with message ID: {response.MessageId}");
    }
}