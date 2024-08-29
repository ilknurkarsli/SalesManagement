using BaSalesManagementApp.Dtos.MailDTOs;

namespace BaSalesManagementApp.Business.Interfaces
{
    /// <summary>
    /// E-posta gönderme işlevselliğini sağlayan arayüz.
    /// </summary>
    public interface IMailService
    {
        /// <summary>
        /// Belirtilen posta bilgilerini kullanarak e-posta gönderir.
        /// </summary>
        /// <param name="mailDto">Gönderilecek e-posta bilgilerini içeren MailCreateDto nesnesi.</param>
        Task SendMailAsync(MailCreateDto mailDto);
    }
}
