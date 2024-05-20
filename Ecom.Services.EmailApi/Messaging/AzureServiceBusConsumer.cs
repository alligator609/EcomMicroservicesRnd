using System.Text;
using Azure.Messaging.ServiceBus;
using Ecom.Services.EmailApi.Models.Dtos;
using Ecom.Services.EmailApi.Services;
using Newtonsoft.Json;

namespace Ecom.Services.EmailApi.Messaging
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        private IConfiguration _configuration;
        private readonly string emailCartQueue;
        private readonly string serviceBusConnectionString;
        private readonly EmailService _emailService;


        private ServiceBusProcessor _emailCartProcessor;

        public AzureServiceBusConsumer(IConfiguration configuration, EmailService emailService)
        {
            _configuration = configuration;
            serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
            emailCartQueue = _configuration.GetValue<string>("TopicQueueNames:EmailShoppingCart");

            var client = new ServiceBusClient(serviceBusConnectionString);
            _emailCartProcessor = client.CreateProcessor(emailCartQueue);
            _emailService = emailService;

        }

        public async Task Start()
        {
            _emailCartProcessor.ProcessMessageAsync += OnEmailCartRequestReceived;
            _emailCartProcessor.ProcessErrorAsync += ErrorHandler;
            await _emailCartProcessor.StartProcessingAsync();
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }

        private async Task OnEmailCartRequestReceived(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);
            var cart = JsonConvert.DeserializeObject<CartDto>(body);
            try
            {
                //var email = new Email
                //{
                //    To = cart.Email,
                //    Subject = "Your shopping cart",
                //    Body = $"Your shopping cart has {cart.Items.Count} items"
                //};

                //await SendEmail(email);
                _emailService.EmailCartLod(cart);
                await args.CompleteMessageAsync(args.Message);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task Stop()
        {
            await _emailCartProcessor.StopProcessingAsync();
            await _emailCartProcessor.DisposeAsync();
        }
    }
}
