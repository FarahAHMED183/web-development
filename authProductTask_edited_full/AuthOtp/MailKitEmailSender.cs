using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Options;

namespace AuthOtp
{
    public class MailKitOptions
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string From { get; set; }
        public string FromName { get; set; }
    }

    public class MailKitEmailSender : IEmailSender
    {
        private readonly MailKitOptions _opts;
        public MailKitEmailSender(IOptions<MailKitOptions> opts)
        {
            _opts = opts.Value;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var msg = new MimeMessage();
            msg.From.Add(new MailboxAddress(_opts.FromName ?? _opts.From, _opts.From));
            msg.To.Add(MailboxAddress.Parse(to));
            msg.Subject = subject;
            msg.Body = new TextPart("html") { Text = body };

            using var client = new SmtpClient();
            await client.ConnectAsync(_opts.Host, _opts.Port, _opts.UseSsl);
            if (!string.IsNullOrEmpty(_opts.User))
                await client.AuthenticateAsync(_opts.User, _opts.Password);
            await client.SendAsync(msg);
            await client.DisconnectAsync(true);
        }
    }
}
