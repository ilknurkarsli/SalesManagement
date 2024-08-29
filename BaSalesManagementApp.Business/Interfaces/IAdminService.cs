using BaSalesManagementApp.Dtos.AdminDTOs;

namespace BaSalesManagementApp.Business.Interfaces
{
    /// <summary>
    /// Admin işlemlerini yöneten servis arayüzü.
    /// </summary>
    public interface IAdminService
    {
        /// <summary>
        /// Yeni bir admin ekler.
        /// </summary>
        /// <param name="adminCreateDTO">Eklenen adminin bilgileri</param>
        /// <returns>Eklenen adminin verilerini ve sonuç durumunu döndürür</returns>
        Task<IDataResult<AdminDTO>> AddAsync(AdminCreateDTO adminCreateDTO);


        /// <summary>
        /// Belirtilen ID'ye sahip adminin getirir.
        /// </summary>
        /// <param name="adminId">Getirilecek adminin ID'si</param>
        /// <returns>Belirtilen ID'ye sahip adminin verilerini ve sonuç durumunu döndürür</returns>
        Task<IDataResult<AdminDTO>> GetByIdAsync(Guid adminId);

        /// <summary>
        /// Tüm adminleri getirir.
        /// </summary>
        /// <returns>Tüm adminlerin listesini ve sonuç durumunu döndürür</returns>
        Task<IDataResult<List<AdminListDTO>>> GetAllAsync();

        /// <summary>
        /// Tüm adminleri getirir ve sıralama seçeneğine göre sıralar.
        /// </summary>
        /// <param name="sortAdmin">Sıralama düzeni (örn. "name", "date")</param>
        /// <returns>Tüm adminlerin listesini ve sonuç durumunu döndürür</returns>
        Task<IDataResult<List<AdminListDTO>>> GetAllAsync(string sortAdmin);

        /// <summary>
        /// Belirtilen ID'ye sahip admini siler.
        /// </summary>
        /// <param name="adminId">Silinecek adminin ID'si</param>
        /// <returns>Admin silme işleminin sonuç durumunu döndürür</returns>
        Task<IResult> DeleteAsync(Guid adminId);

        /// <summary>
        /// Belirtilen ID'ye sahip admini verilen bilgilerle günceller.
        /// </summary>
        /// <param name="adminUpdateDTO">Güncellenecek adminin bilgileri</param>
        /// <returns>Güncellenen yöneadmininticinin verilerini ve sonuç durumunu döndürür</returns>
        Task<IDataResult<AdminDTO>> UpdateAsync(AdminUpdateDTO adminUpdateDTO);
    }
}
