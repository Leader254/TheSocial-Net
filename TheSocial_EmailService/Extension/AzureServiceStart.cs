
using TheSocial_EmailService.Messaging;

namespace TheSocial_EmailService.Extension
{
    public static class AzureServiceStart
    {
        public static IMessageBusConsumer messageBusConsumer { get; set; }
        public static IApplicationBuilder useAzure(this IApplicationBuilder app)
        {
           messageBusConsumer = app.ApplicationServices.GetService<IMessageBusConsumer>();

            var life = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            life.ApplicationStarted.Register(OnStarted);
            life.ApplicationStopped.Register(OnStopped);

            return app;
        }

        private static void OnStarted()
        {
            //start email processing
            messageBusConsumer.Start();
        }

        private static void OnStopped()
        {
            //stop email processing
            messageBusConsumer.Stop();
        }
    }
}
