namespace BaSalesManagementApp.Business.Interfaces
{
    
    public interface IQrService
    {
        /// <summary>
        /// Belirtilen anahtarı kullanarak bir QR kodu üretir.
        /// </summary>
        /// <param name="id">QR kodu için kullanılacak olan Id.</param>
        /// <returns></returns>
        byte[] GenerateQrCode(string id);
    }
}
