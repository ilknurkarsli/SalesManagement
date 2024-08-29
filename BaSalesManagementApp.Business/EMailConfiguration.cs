namespace BaSalesManagementApp.Business
{
    /// <summary>
    /// E-posta gönderme işlemi için gereken yapılandırma bilgilerini içeren sınıf.
    /// </summary>
    public class EMailConfiguration
    {
        /// <summary>
        /// E-postaların hangi e-posta adresinden gönderileceğini belirtir.
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// SMTP sunucusunun adresini belirtir.
        /// </summary>
        public string SmtpServer { get; set; }

        /// <summary>
        /// SMTP sunucusuna bağlanmak için kullanılan port numarasını belirtir.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// SMTP sunucusuna kimlik doğrulama için kullanılan kullanıcı adını belirtir.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// SMTP sunucusuna kimlik doğrulama için kullanılan şifreyi belirtir.
        /// </summary>
        public string Password { get; set; }
    }
}
