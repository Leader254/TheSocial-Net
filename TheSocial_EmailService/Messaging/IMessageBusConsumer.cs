namespace TheSocial_EmailService.Messaging
{
    public interface IMessageBusConsumer
    {
        Task Start();
        Task Stop();
    }
}
