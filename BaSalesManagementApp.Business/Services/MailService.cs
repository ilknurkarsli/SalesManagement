using BaSalesManagementApp.Dtos.MailDTOs;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace BaSalesManagementApp.Business.Services
{
    /// <summary>
    /// E-posta gönderme işlevselliğini sağlayan servis.
    /// </summary>
    public class MailService : IMailService
    {
        private readonly EMailConfiguration _emailConfig;

        /// <summary>
        /// MailService sınıfını başlatırken kullanılacak yapılandırma değerlerini alır.
        /// </summary>
        /// <param name="emailConfig">E-posta yapılandırma değerlerini içeren IOptions nesnesi.</param>
        public MailService(IOptions<EMailConfiguration> emailConfig)
        {
            _emailConfig = emailConfig.Value;
        }

        /// <summary>
        /// Belirtilen posta bilgilerini kullanarak e-posta gönderir.
        /// </summary>
        /// <param name="mailDto">Gönderilecek e-posta bilgilerini içeren MailCreateDto nesnesi.</param>
        /// <returns>Asenkron bir işlem olan e-posta gönderme işleminin sonucunu temsil eden Task.</returns>
        public async Task SendMailAsync(MailCreateDto mailDto)
        {
            try
            {
            var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_emailConfig.From));
                email.To.Add(MailboxAddress.Parse(mailDto.ReceiverMailAddress));
                email.Subject = mailDto.Subject;

                var builder = new BodyBuilder();
                builder.HtmlBody = mailDto.Body;
                email.Body = builder.ToMessageBody();

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_emailConfig.Username, _emailConfig.Password);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
        }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"E-posta gönderilirken bir hata oluştu: {ex.Message}");
            }
        }
    }
}

