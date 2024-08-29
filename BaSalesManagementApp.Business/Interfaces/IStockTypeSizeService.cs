using BaSalesManagementApp.Dtos.StockTypeSizeDTOs;
using BaSalesManagementApp.Dtos.WarehouseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaSalesManagementApp.Business.Interfaces
{
    public interface IStockTypeSizeService
    {
        /// <summary>
        /// Yeni bir Stok Tipi Boyutu oluşturur.
        /// </summary>
        /// <param name="stockTypeSizeCreateDTO"></param>
        /// <returns></returns>
        Task<IDataResult<StockTypeSizeDTO>> AddAsync(StockTypeSizeCreateDTO stockTypeSizeCreateDTO);

        /// <summary>
        /// Belirtilen Id ye göre ilgili Stok Tipi Boyutunu getirir.
        /// </summary>
        /// <param name="stockTypeSizeId"></param>
        /// <returns></returns>
        Task<IDataResult<StockTypeSizeDTO>> GetByIdAsync(Guid stockTypeSizeId);

        /// <summary>
        /// Belirtilen Id ye ait Stok Tipi Boyutunu siler.
        /// </summary>
        /// <param name="stockTypeSizeId"></param>
        /// <returns></returns>
        Task<IResult> DeleteAsync(Guid stockTypeSizeId);

        /// <summary>
        /// Tüm Stok Tipi Boyutlarını getirir.
        /// </summary>
        /// <returns></returns>
        Task<IDataResult<List<StockTypeSizeDTO>>> GetAllAsync();

        /// <summary>
        /// Belirtilen Stok Tipi Boyutunu günceller.
        /// </summary>
        /// <param name="stockTypeSizeUpdateDTO"></param>
        /// <returns></returns>
        Task<IResult> UpdateAsync(StockTypeSizeUpdateDTO stockTypeSizeUpdateDTO);
    }
}

