using BaSalesManagementApp.Dtos.ProductTypeDtos;

namespace BaSalesManagementApp.Business.Interfaces
{
    public interface IStockTypeService
    {
        /// <summary>
        /// Yeni bir ürün tipi ekler.
        /// </summary>
        /// <param name="productTypeAddDto">Eklenmek istenen ürün tipi ile ilgili verileri içeren veri transfer nesnesi.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda eklenen ürün tipi verilerini geri döndürür.</returns>
        Task<IDataResult<StockTypeDto>> AddAsync(StockTypeAddDto productTypeAddDto);

        /// <summary>
        /// Benzer kimliği ile bir ürün tipini siler.
        /// </summary>
        /// <param name="productTypeId">Silinmek istenen ürün tipinin benzersiz kimliği.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda silme işleminin başarılı olup olmadığı bilgisini döner.</returns>
        Task<IResult> DeleteAsync(Guid productTypeId);

        /// <summary>
        /// Bir ürün tipini günceller.
        /// </summary>
        /// <param name="productTypeUpdateDto">Güncellenmiş ürün tipi ile ilgili verileri döner.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda güncellenen ürün tipi verilerini döndürür.</returns>
        Task<IDataResult<StockTypeDto>> UpdateAsync(StockTypeUpdateDto productTypeUpdateDto);

        /// <summary>
        /// Benzersiz kimliği ile bir ürün tipini getirir.
        /// </summary>
        /// <param name="productTypeId">Getirilmek istenen ürün tipinin benzersiz kimliği.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda varsa eğer ürün tipini döndürür yoksa null döner.</returns>
        Task<IDataResult<StockTypeDto>> GetByAsync(Guid productTypeId);

        /// <summary>
        /// Tüm ürün tiplerini getirir.
        /// </summary>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda varsa eğer tüm ürün tiplerini getirir yoksa null döner.</returns>
        Task<IDataResult<IEnumerable<StockTypeListDto>>> GetAllAsync();
    }
}