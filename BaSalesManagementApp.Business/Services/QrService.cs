using BaSalesManagementApp.Dtos.ProductDTOs;
using System.Collections;
using System.Drawing;
using ZXing.Common;
using ZXing.QrCode;
using static System.Formats.Asn1.AsnWriter;

namespace BaSalesManagementApp.Business.Services
{
    public class QrService : IQrService
    {
        /// <summary>
        /// Belirtilen anahtarı kullanarak bir QR kodu üretir.
        /// </summary>
        /// <param name="id">QR kodu için kullanılacak olan Id.</param>
        /// <returns>Üretilen QR kodunun byte dizisi olarak temsil edilen PNG görüntüsü.</returns>
        public byte[] GenerateQrCode(string id)
        {
            try
            {
                var width = 200;
                var height = 200;
                var margin = 0;
                var qrCodeWriter = new ZXing.BarcodeWriterPixelData
                {
                    Format = ZXing.BarcodeFormat.QR_CODE,
                    Options = new QrCodeEncodingOptions
                    {
                        Height = height,
                        Width = width,
                        Margin = margin
                    }
                };
                var pixelData = qrCodeWriter.Write(id);

                using (var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
                {
                    using (var ms = new MemoryStream())
                    {
                        var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                        try
                        {
                            System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                        }
                        finally
                        {
                            bitmap.UnlockBits(bitmapData);
                        }
                        bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        return ms.ToArray();
                    }
                }

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("QR kod oluşturma sırasında bir hata oluştu.", ex);
            }

        }
    }
}
