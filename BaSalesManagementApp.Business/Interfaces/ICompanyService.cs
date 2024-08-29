using BaSalesManagementApp.Dtos.CompanyDTOs;

namespace BaSalesManagementApp.Business.Interfaces
{
    public interface ICompanyService
    {
        /// <summary>
        /// Yeni bir firma ekler.
        /// </summary>
        /// <param name="companyCreateDTO">Eklenmek istenen firmayla ilgili bilgileri içeren veri transfer nesnesi.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda eklenen şube verilerini içerir.</returns>
        Task<IDataResult<CompanyDTO>> AddAsync(CompanyCreateDTO companyCreateDTO);

        /// <summary>
        /// Benzersiz kimliğiyle bir firmayı alır.
        /// </summary>
        /// <param name="companyId">Alınmak istenen firmanın benzersiz kimliği.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda firma verilerini içerir, bulunamazsa null döner.</returns>
        /// 
        Task<IDataResult<CompanyDTO>> GetByIdAsync(Guid companyId);

        /// <summary>
        /// Benzersiz kimliğiyle bir firmayı siler.
        /// </summary>
        /// <param name="companyId">Silinmek istenen firmanın benzersiz kimliği.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda silme işleminin başarılı olup olmadığını belirtir.</returns>
        Task<IResult> DeleteAsync(Guid companyId);

        /// <summary>
        /// Tüm firmaları alır.
        /// </summary>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda tüm firmaların listesini içerir.</returns>
        Task<IDataResult<List<CompanyListDTO>>> GetAllAsync();

        /// <summary>
        /// Bir firmayı günceller.
        /// </summary>
        /// <param name="companyUpdateDTO">Güncellenmiş firmayla ilgili bilgileri içeren veri transfer nesnesi.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda güncellenmiş firma verilerini içerir.</returns>
        Task<IDataResult<CompanyDTO>> UpdateAsync(CompanyUpdateDTO companyUpdateDTO);
    }
}
