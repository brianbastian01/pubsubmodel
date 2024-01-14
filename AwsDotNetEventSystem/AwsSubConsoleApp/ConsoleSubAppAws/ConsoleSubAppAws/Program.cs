using Amazon;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Amazon.SQS;
using Amazon.SQS.Model;

class Program
{
    static async Task Main(string[] args)
    {
        var snsClient = new AmazonSimpleNotificationServiceClient(RegionEndpoint.YOUR_REGION);
        var sqsClient = new AmazonSQSClient(RegionEndpoint.YOUR_REGION);

        var queueUrl = "YOUR_QUEUE_URL";

        var subscribeRequest = new SubscribeRequest
        {
            Protocol = "sqs",
            Endpoint = queueUrl,
            TopicArn = "YOUR_TOPIC_ARN"
        };

        var subscribeResponse = await snsClient.SubscribeAsync(subscribeRequest);

        Console.WriteLine($"Subscribed to topic with subscription ARN: {subscribeResponse.SubscriptionArn}");

        while (true)
        {
            var receiveMessageRequest = new ReceiveMessageRequest
            {
                QueueUrl = queueUrl,
                MaxNumberOfMessages = 1
            };

            var receiveMessageResponse = await sqsClient.ReceiveMessageAsync(receiveMessageRequest);

            foreach (var message in receiveMessageResponse.Messages)
            {
                Console.WriteLine($"Received message: {message.Body}");

                // Process the message as needed

                // Delete the message from the queue
                await sqsClient.DeleteMessageAsync(queueUrl, message.ReceiptHandle);
            }

            Thread.Sleep(1000); // Add a delay to avoid constant polling
        }
    }
}