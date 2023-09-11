using MailKit.Net.Smtp;
using MimeKit;
using TheSocial_EmailService.Models.Dtos;

namespace TheSocial_EmailService.SendMail
{
    public class SendMailService
    {
        public async Task SendMail(UserMessage res, string message)
        {
            MimeMessage message1 = new MimeMessage();
            message1.From.Add(new MailboxAddress("SocialApp", "devsamuel219@gmail.com"));

            message1.To.Add(new MailboxAddress(res.Name, res.Email));

            message1.Subject = "Welcome to TheSocial";

            var body = new TextPart("html")
            {
                Text = message.ToString()
            };

            message1.Body = body;
            var client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("devsamuel219@gmail.com", "ejjmpuzcrchugowe");
            await client.SendAsync(message1);
            await client.DisconnectAsync(true);
        }
    }
}
