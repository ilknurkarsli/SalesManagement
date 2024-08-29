using BaSalesManagementApp.Dtos.CityDTOs;
using BaSalesManagementApp.Dtos.CountryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaSalesManagementApp.Business.Interfaces
{
    public interface ICityService
    {
        /// <summary>
        /// Tüm Cityleri getirir.
        /// </summary>
        /// <returns>Tüm Citylerin listesini ve sonuç durumunu döndürür</returns>
        Task<IDataResult<List<CityListDTO>>> GetAllAsync(string orderOrder);
        
                /// <summary>
                /// Yeni bir şehir ekler.
                /// </summary>
                /// <param name="cityCreateDTO">Eklenmek istenen şehir ilgili bilgileri içeren veri transfer nesnesi.</param>
                /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda eklenen şehir verilerini içerir.</returns>
                Task<IDataResult<CityDTO>> AddAsync(CityCreateDTO cityCreateDTO);

        /// <summary>
        /// Benzersiz kimliğiyle bir şehir alır.
        /// </summary>
        /// <param name="cityId">Alınmak istenen şehrin benzersiz kimliği.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda şehir verilerini içerir, bulunamazsa null döner.</returns>
        Task<IDataResult<CityDTO>> GetByIdAsync(Guid cityId);

        /// <summary>
        /// Benzersiz kimliğiyle bir şehri siler.
        /// </summary>
        /// <param name="cityId">Silinmek istenen şehrin benzersiz kimliği.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda silme işleminin başarılı olup olmadığını belirtir.</returns>
        Task<IResult> DeleteAsync(Guid cityId);

        /// <summary>
        /// Tüm şehirleri alır.
        /// </summary>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda tüm şehirlerin listesini içerir.</returns>
        Task<IDataResult<List<CityListDTO>>> GetAllAsync();

        /// <summary>
        /// Bir şehri günceller.
        /// </summary>
        /// <param name="cityUpdateDTO">Güncellenmiş şehir ile ilgili bilgileri içeren veri transfer nesnesi.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda güncellenmiş şehir verilerini içerir.</returns>
        Task<IDataResult<CityDTO>> UpdateAsync(CityUpdateDTO cityUpdateDTO);

        public bool CityExist(string cityName);
    }
}
