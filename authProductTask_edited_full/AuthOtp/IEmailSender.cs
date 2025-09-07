using System.Threading.Tasks;

namespace AuthOtp
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}
