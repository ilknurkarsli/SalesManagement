using BaSalesManagementApp.Dtos.StockDTOs;

namespace BaSalesManagementApp.Business.Interfaces
{
    public interface IStockService
    {
        /// <summary>
        /// Tüm stokları getirir.
        /// </summary>
        /// <returns>Tüm stokların listesini ve sonuç durumunu döndürür</returns>
        Task<IDataResult<List<StockListDTO>>> GetAllAsync(string orderOrder);

        Task<IDataResult<List<StockListDTO>>> GetAllAsync();

        /// <summary>
        /// Belirtilen ID'li stoğu getirir.
        /// </summary>
        /// <param name="stockId">Getirilecek stok ID'si</param>
        /// <returns>Belirtilen ID'li stok verilerini ve sonuç durumunu döndürür</returns>
        Task<IDataResult<StockDTO>> GetByIdAsync(Guid stockId);

        /// <summary>
        /// Yeni bir stok oluşturur.
        /// </summary>
        /// <param name="stockCreateDTO">Oluşturulacak stok bilgileri</param>
        /// <returns>Oluşturulan stok verilerini ve sonuç durumunu döndürür</returns>
        Task<IDataResult<StockDTO>> AddAsync(StockCreateDTO stockCreateDTO);

        /// <summary>
        /// ID'sine karşılık gelen stok verilen özellikler ile güncellenir
        /// </summary>
        /// <param name="stockUpdateDTO">Güncellenecek stok ID'si</param>
        /// <returns></returns>
        Task<IDataResult<StockDTO>> UpdateAsync(StockUpdateDTO stockUpdateDTO);

        /// <summary>
        /// Belirtilen ID'li stok silinir.
        /// </summary>
        /// <param name="stockId">Silinecek stok ID'si</param>
        /// <returns>Stok silme işleminin sonuç durumunu döndürür</returns>
        Task<IResult> DeleteAsync(Guid stockId);
    }
}
