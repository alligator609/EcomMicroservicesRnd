using System.Reflection.Metadata;
using Ecom.Services.EmailApi.Messaging;

namespace Ecom.Services.EmailApi.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IAzureServiceBusConsumer azureServiceBusConsumer { get; set; }
        public static IApplicationBuilder UseAzureServiceBusConsumer (this IApplicationBuilder app)
        {
            azureServiceBusConsumer = app.ApplicationServices.GetService<IAzureServiceBusConsumer>();
            var hostApplicationLife = app.ApplicationServices.GetService<IHostApplicationLifetime>();
            hostApplicationLife.ApplicationStarted.Register(OnStart);
            hostApplicationLife.ApplicationStopped.Register(OnStop);
            return app;
        }

        private static void OnStop()
        {
            azureServiceBusConsumer.Stop();
        }

        private static void OnStart()
        {
            azureServiceBusConsumer.Start();
        }
    }
}