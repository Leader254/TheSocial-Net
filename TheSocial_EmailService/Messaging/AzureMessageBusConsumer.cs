using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System.Text;
using TheSocial_EmailService.Models.Dtos;
using TheSocial_EmailService.SendMail;

namespace TheSocial_EmailService.Messaging
{
    public class AzureMessageBusConsumer : IMessageBusConsumer
    {
        private readonly IConfiguration _configuration;
        private readonly string ConnectionString;
        private readonly string QueueName;
        private readonly ServiceBusProcessor _serviceProcessor;
        private readonly SendMailService _sendMailService;
        public AzureMessageBusConsumer(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetSection("ServiceBus:ConnectionString").Get<string>();
            QueueName = _configuration.GetSection("QueuesandTopic:RegisterUser").Get<string>();
            var serviceBusClient = new ServiceBusClient(ConnectionString);
            _serviceProcessor = serviceBusClient.CreateProcessor(QueueName);
            _sendMailService = new SendMailService();
        }

        public async Task Start()
        {
            _serviceProcessor.ProcessMessageAsync += OnRegistration;
            _serviceProcessor.ProcessErrorAsync += ErrorHandler;
            await _serviceProcessor.StartProcessingAsync();
        }
        public async Task Stop()
        {
            await _serviceProcessor.StopProcessingAsync();
            await _serviceProcessor.DisposeAsync();
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            throw new NotImplementedException();
        }

        private async Task OnRegistration(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);
            var userMessage = JsonConvert.DeserializeObject<UserMessage>(body);

            //send email
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"<h1>Hi {userMessage.Name},</h1>");
                sb.Append($"<h2>Thank you for registering with us.</h2>");
                sb.AppendLine("<br/>");
                //new line
                sb.Append($"<p>Regards,</p>");
                sb.Append($"<p>TheSocial App</p>");
                await _sendMailService.SendMail(userMessage, sb.ToString());
                await args.CompleteMessageAsync(message);
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
