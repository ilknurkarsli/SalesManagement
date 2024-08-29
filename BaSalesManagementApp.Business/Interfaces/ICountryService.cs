using BaSalesManagementApp.Dtos.BranchDTOs;
using BaSalesManagementApp.Dtos.CountryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaSalesManagementApp.Business.Interfaces
{
    public interface ICountryService
    {

        /// <summary>
        /// Yeni bir şube ekler.
        /// </summary>
        /// <param name="countryCreateDTO">Eklenmek istenen ülke ilgili bilgileri içeren veri transfer nesnesi.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda eklenen ülke verilerini içerir.</returns>
        Task<IDataResult<CountryDTO>> AddAsync(CountryCreateDTO countryCreateDTO);

        /// <summary>
        /// Benzersiz kimliğiyle bir ülke alır.
        /// </summary>
        /// <param name="countryId">Alınmak istenen ülkenin benzersiz kimliği.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda ülke verilerini içerir, bulunamazsa null döner.</returns>
        Task<IDataResult<CountryDTO>> GetByIdAsync(Guid countryId);

        /// <summary>
        /// Benzersiz kimliğiyle bir ülkeyi siler.
        /// </summary>
        /// <param name="countryId">Silinmek istenen ülkenin benzersiz kimliği.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda silme işleminin başarılı olup olmadığını belirtir.</returns>
        Task<IResult> DeleteAsync(Guid countryId);

        /// <summary>
        /// Tüm ülkeleri alır.
        /// </summary>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda tüm ülkelerin listesini içerir.</returns>
        Task<IDataResult<List<CountryListDTO>>> GetAllAsync();

        /// <summary>
        /// Bir ülkeyi günceller.
        /// </summary>
        /// <param name="countryUpdateDTO">Güncellenmiş ülke ilgili bilgileri içeren veri transfer nesnesi.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda güncellenmiş ülke verilerini içerir.</returns>
        Task<IDataResult<CountryDTO>> UpdateAsync(CountryUpdateDTO countryUpdateDTO);


        /// <summary>
        /// Bir ülkeyi günceller.
        /// </summary>
        /// <param name="countryName">Ülke ile ilgili isim bilgisi.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda VeriTabanında aynı isimden bir veri var mı kontrol eder.</returns>
        public bool CountryExist(string countryName);
    }
}
