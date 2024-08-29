using BaSalesManagementApp.Dtos.OrderDetailDTOs;
using BaSalesManagementApp.Dtos.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaSalesManagementApp.Business.Interfaces
{
    public interface IOrderDetailService
    {
        /// <summary>
        /// Tüm siparişleri getirir.
        /// </summary>
        /// <returns>Tüm siparişlerin listesini ve sonuç durumunu döndürür</returns>
        Task<IDataResult<List<OrderDetailListDTO>>> GetAllAsync();

        /// <summary>
        /// Belirtilen ID'li siparişi getirir.
        /// </summary>
        /// <param name="orderId">Getirilecek siparişin ID'si</param>
        /// <returns>Belirtilen ID'li siparişin verilerini ve sonuç durumunu döndürür</returns>
        Task<IDataResult<OrderDetailDTO>> GetByIdAsync(Guid orderDetailId);

        /// <summary>
        /// Yeni bir sipariş oluşturur.
        /// </summary>
        /// <param name="orderCreateDTO">Oluşturulacak siparişin bilgileri</param>
        /// <returns>Oluşturulan siparişin verilerini ve sonuç durumunu döndürür</returns>
        Task<IDataResult<OrderDetailDTO>> AddAsync(OrderDetailCreateDTO orderDetailCreateDTO);

        /// <summary>
        /// ID'sine karşılık gelen siparişi verilen özellikler ile günceller
        /// </summary>
        /// <param name="orderUpdateDTO">Güncellenecek siparişin ID'si</param>
        /// <returns></returns>
        Task<IDataResult<OrderDetailDTO>> UpdateAsync(OrderDetailUpdateDTO orderDetailUpdateDTO);

        /// <summary>
        /// Belirtilen ID'li siparişi siler.
        /// </summary>
        /// <param name="orderId">Silinecek siparişin ID'si</param>
        /// <returns>Siparişi silme işleminin sonuç durumunu döndürür</returns>
        Task<IResult> DeleteAsync(Guid orderDetailId);

      
       
        
        
    }


}
