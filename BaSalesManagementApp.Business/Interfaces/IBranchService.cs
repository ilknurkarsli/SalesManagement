using BaSalesManagementApp.Dtos.BranchDTOs;

namespace BaSalesManagementApp.Business.Interfaces
{
    public interface IBranchService
    {
        /// <summary>
        /// Yeni bir şube ekler.
        /// </summary>
        /// <param name="branchCreateDTO">Eklenmek istenen şubeyle ilgili bilgileri içeren veri transfer nesnesi.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda eklenen şube verilerini içerir.</returns>
        Task<IDataResult<BranchDTO>> AddAsync(BranchCreateDTO branchCreateDTO);

        /// <summary>
        /// Benzersiz kimliğiyle bir şubeyi alır.
        /// </summary>
        /// <param name="branchId">Alınmak istenen şubenin benzersiz kimliği.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda şube verilerini içerir, bulunamazsa null döner.</returns>
        Task<IDataResult<BranchDTO>> GetByIdAsync(Guid branchId);

        /// <summary>
        /// Benzersiz kimliğiyle bir şubeyi siler.
        /// </summary>
        /// <param name="branchId">Silinmek istenen şubenin benzersiz kimliği.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda silme işleminin başarılı olup olmadığını belirtir.</returns>
        Task<IResult> DeleteAsync(Guid branchId);

        /// <summary>
        /// Tüm şubeleri alır.
        /// </summary>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda tüm şubelerin listesini içerir.</returns>
        Task<IDataResult<List<BranchListDTO>>> GetAllAsync();

        /// <summary>
        /// Bir şubeyi günceller.
        /// </summary>
        /// <param name="branchUpdateDTO">Güncellenmiş şubeyle ilgili bilgileri içeren veri transfer nesnesi.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda güncellenmiş şube verilerini içerir.</returns>
        Task<IDataResult<BranchDTO>> UpdateAsync(BranchUpdateDTO branchUpdateDTO);

        /// <summary>
        /// Company kimliğine ait ranchleri listeler.
        /// </summary>
        /// <param name="companyId">Branchlerine ulaşılmak istenen companynin benzersiz kimliği</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda ilgili companye ait branchleri listeler</returns>
        Task<List<Branch>> GetBranchesByCompanyIdAsync(Guid companyId);

    }
}

