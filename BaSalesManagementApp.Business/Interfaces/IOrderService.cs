using BaSalesManagementApp.Dtos.OrderDTOs;

namespace BaSalesManagementApp.Business.Interfaces
{
    public interface IOrderService
    {
        /// <summary>
        /// Tüm siparişleri getirir.
        /// </summary>
        /// <returns>Tüm siparişlerin listesini ve sonuç durumunu döndürür</returns>
        Task<IDataResult<List<OrderListDTO>>> GetAllAsync();
        Task<IDataResult<List<OrderListDTO>>> GetAllAsync(string sortOrder);

        /// <summary>
        /// Belirtilen ID'li siparişi getirir.
        /// </summary>
        /// <param name="orderId">Getirilecek siparişin ID'si</param>
        /// <returns>Belirtilen ID'li siparişin verilerini ve sonuç durumunu döndürür</returns>
        Task<IDataResult<OrderDTO>> GetByIdAsync(Guid orderId);

        /// <summary>
        /// Yeni bir sipariş oluşturur ve siparişi oluştururken IdentityId bilgisini de alır. 
        /// </summary>
        /// <param name="orderCreateDTO">Oluşturulacak siparişin bilgileri</param>
        /// <returns>Oluşturulan siparişin verilerini ve sonuç durumunu döndürür</returns>
        Task<IDataResult<OrderDTO>> AddAsync(OrderCreateDTO orderCreateDTO, string identityId);

        /// <summary>
        /// ID'sine karşılık gelen siparişi verilen özellikler ile günceller
        /// </summary>
        /// <param name="orderUpdateDTO">Güncellenecek siparişin ID'si</param>
        /// <returns></returns>
        Task<IDataResult<OrderDTO>> UpdateAsync(OrderUpdateDTO orderUpdateDTO);

        /// <summary>
        /// Belirtilen ID'li siparişi siler.
        /// </summary>
        /// <param name="orderId">Silinecek siparişin ID'si</param>
        /// <returns>Siparişi silme işleminin sonuç durumunu döndürür</returns>
        Task<IResult> DeleteAsync(Guid orderId);


    }
}
