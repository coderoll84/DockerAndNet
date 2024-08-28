using System.Net.Mail;

namespace Api.Servicios
{
    public class EmailSender(IConfiguration config) : IEmailSender
    {
        private readonly string _smtpServer = config.GetValue<string>("Email:SmtpServer");
        private readonly int _smtpPort = config.GetValue<int>("Email:SmtpPort");
        private readonly string _fromAddress = config.GetValue<string>("Email:SenderAddress");

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MailMessage
            {
                From = new MailAddress(_fromAddress),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };
            message.To.Add(new MailAddress(email));

            using var client = new SmtpClient(_smtpServer, _smtpPort);
            await client.SendMailAsync(message);
        }
    }
}
