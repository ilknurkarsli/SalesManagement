using BaSalesManagementApp.Dtos.PromotionDTOs;

namespace BaSalesManagementApp.Business.Interfaces
{
    public interface IPromotionService
    {
        /// <summary>
        /// Yeni bir Promosyon ekler
        /// </summary>
        /// <param name="promotionCreateDTO"> Yeni Oluşturulacak Promosyonun bilgilerini içeren veri transfer nesnesi</param>
        /// <returns>Oluşturulan Promotion'ın bilgilerini döndürür</returns>
        Task<IDataResult<PromotionDTO>> AddAsync(PromotionCreateDTO promotionCreateDTO);

        /// <summary>
        /// Promosyon bilgilerini günceller
        /// </summary>
        /// <param name="promotionUpdateDTO">Güncellenecek Promosyonun bilgilerini içeren veri transfer nesnesi</param>
        /// <returns>Güncellenen Promotion'ın bilgilerini döndürür</returns>
        Task<IDataResult<PromotionDTO>> UpdateAsync(PromotionUpdateDTO promotionUpdateDTO);

        /// <summary>
        /// Id 'si belirtilen Promosyonu siler
        /// </summary>
        /// <param name="promotionId">silinmek istenen promosyonun Id'si</param>
        /// <returns>Silme işleminin sonucunu döndürür</returns>
        Task<IResult> DeleteAsync(Guid promotionId);

        /// <summary>
        /// Id 'si belirtilen Promosyon bilgilerini getirir
        /// </summary>
        /// <param name="promotionId">çağırılan promosyonun Id'si</param>
        /// <returns>Promosyon bilgilerini döndürür</returns>
        Task<IDataResult<PromotionDTO>> GetByIdAsync(Guid promotionId);

        /// <summary>
        /// Tüm Promosyon bilgilerini içeren Listeyi getirir
        /// </summary>
        /// <returns>Tüm Promosyon bilgilerini döndürür</returns>
        Task<IDataResult<List<PromotionListDTO>>> GetAllAsync();

        /// <summary>
        /// Tüm Promosyon bilgilerini içeren Listeyi getirir
        /// </summary>
        /// <param name="sortOrder">Promosyon listesinin düzenini belirler</param>
        /// <returns>Tüm Promosyon bilgilerini döndürür</returns>
        Task<IDataResult<List<PromotionListDTO>>> GetAllAsync(string sortOrder);

        /// <summary>
        /// Belirtilen Job ID ile ilgili promosyon güncellemelerini bildirir.
        /// </summary>
        /// <param name="jobId">Job ID'si</param>
        /// <returns>Asenkron bir görev döndürür</returns>
        Task NotifyPromotionUpdatesWithJobId(string jobId);
	}
}
