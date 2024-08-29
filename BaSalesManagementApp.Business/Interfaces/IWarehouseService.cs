using BaSalesManagementApp.Dtos.WarehouseDTOs;

namespace BaSalesManagementApp.Business.Interfaces
{
    public interface IWarehouseService
    {
        /// <summary>
        /// Tüm WareHouse getirir.
        /// /// <param name="sortOrder">Depo listesinin sıralama düzenini belirten parametre.
        /// </summary>
        /// <returns>Tüm WareHouse listesini ve sonuç durumunu döndürür</returns>
        Task<IDataResult<List<WarehouseListDTO>>> GetAllAsync(string sortOrder);
        /// <summary>
        /// Yeni bir Depo oluşturur.
        /// </summary>
        /// <param name="warehouseCreateDTO"></param>
        /// <returns></returns>
        Task<IDataResult<WarehouseDTO>> AddAsync(WarehouseCreateDTO warehouseCreateDTO);

        /// <summary>
        /// Belirtilen Id ye göre ilgili depoyu getirir.
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        Task<IDataResult<WarehouseDTO>> GetByIdAsync(Guid warehouseId);

        /// <summary>
        /// Belirtilen Id ye ait depoyu siler.
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        Task<IResult> DeleteAsync(Guid warehouseId);

        /// <summary>
        /// Tüm depoları getirir.
        /// </summary>
        /// <returns></returns>
        Task<IDataResult<List<WarehouseListDTO>>> GetAllAsync();

        /// <summary>
        /// Belirtilen depoyu günceller.
        /// </summary>
        /// <param name="warehouseUpdateDTO"></param>
        /// <returns></returns>
        Task<IResult> UpdateAsync(WarehouseUpdateDTO warehouseUpdateDTO);
    }
}
