using BaSalesManagementApp.Dtos.OrderDTOs;

namespace BaSalesManagementApp.Business.Services
{
    /// <summary>
    /// OrderService sınıfı, siparişlerle ilgili CRUD işlemlerini gerçekleştirir.
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;


        /// <summary>
        /// OrderService kurucusu, IOrderRepository bağımlılığını alır.
        /// </summary>
        /// <param name="orderRepository">Sipariş deposu</param>
        public OrderService(IOrderRepository orderRepository,  IOrderDetailRepository orderDetailRepository, IAdminRepository adminRepository)
        {
            _orderRepository = orderRepository;
            _adminRepository = adminRepository;
            _orderDetailRepository = orderDetailRepository;
            _adminRepository = adminRepository;
        }

        /// <summary>
        /// Tüm siparişleri getirir.
        /// </summary>
        /// <returns>Tüm siparişlerin listesi</returns>
        public async Task<IDataResult<List<OrderListDTO>>> GetAllAsync()
        {
            try
            {
                var orders = await _orderRepository.GetAllWithAdminAsync();
                var orderList = orders.Select(order => new OrderListDTO
                {
                    ProductName = order.Product.Name,
                    Id = order.Id,
                    Quantity = order.Quantity,
                    TotalPrice = order.TotalPrice,
                    OrderDate = order.OrderDate,
                    AdminName = order.Admin.FirstName + " " + order.Admin.LastName // Admin adını ekleyelim
                }).ToList();

                if (orderList == null || orderList.Count == 0)
                {
                    return new ErrorDataResult<List<OrderListDTO>>(orderList, Messages.ORDER_LIST_EMPTY);
                }

                return new SuccessDataResult<List<OrderListDTO>>(orderList,Messages.ORDER_LISTED_SUCCESS);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<OrderListDTO>>(new List<OrderListDTO>(), Messages.ORDER_LIST_FAILED + ex.Message);
            }
        }

        public async Task<IDataResult<List<OrderListDTO>>> GetAllAsync(string sortOrder)
        {
            try
            {
                var orders = await _orderRepository.GetAllWithAdminAsync();
                var orderList = orders.Select(order => new OrderListDTO
                {
                    ProductName = order.Product.Name,
                    Id = order.Id,
                    Quantity = order.Quantity,
                    TotalPrice = order.TotalPrice,
                    OrderDate = order.OrderDate,
                    AdminName = order.Admin.FirstName + " " + order.Admin.LastName // Admin adını ekleyelim
                }).ToList();

                if (orderList == null || orderList.Count == 0)
                {
                    return new ErrorDataResult<List<OrderListDTO>>(orderList, Messages.ORDER_LIST_EMPTY);
                }

                switch (sortOrder.ToLower())
                {
                    case "date":
                        orderList = orderList.OrderByDescending(s => s.OrderDate).ToList();
                        break;
                    case "datedesc":
                        orderList = orderList.OrderBy(s => s.OrderDate).ToList();
                        break;
                    case "alphabetical":
                        orderList = orderList.OrderBy(s => s.ProductName).ToList();
                        break;
                    case "alphabeticaldesc":
                        orderList = orderList.OrderByDescending(s => s.ProductName).ToList();
                        break;
                }

                return new SuccessDataResult<List<OrderListDTO>>(orderList, Messages.ORDER_LISTED_SUCCESS);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<OrderListDTO>>(new List<OrderListDTO>(), Messages.ORDER_LIST_FAILED + ex.Message);
            }
        }

        /// <summary>
        /// Belirtilen ID'li siparişi getirir.
        /// </summary>
        /// <param name="orderId">Getirilecek siparişin ID'si</param>
        /// <returns>Belirtilen ID'li siparişin verileri</returns>
        public async Task<IDataResult<OrderDTO>> GetByIdAsync(Guid orderId)
        {
            try
            {
                var order = await _orderRepository.GetByIdAsync(orderId);                
                if (order == null)
                {
                    return new ErrorDataResult<OrderDTO>(Messages.ORDER_NOT_FOUND);
                }

                return new SuccessDataResult<OrderDTO>(order.Adapt<OrderDTO>(), Messages.ORDER_FOUND_SUCCESS);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<OrderDTO>(Messages.ORDER_GET_FAILED + ex.Message);
            }
        }

        /// <summary>
        /// Yeni bir sipariş oluşturur.
        /// </summary>
        /// <param name="orderCreateDTO">Oluşturulacak sipariş bilgileri</param>
        /// <returns>Sipariş oluşturma işleminin sonucu</returns>
        public async Task<IDataResult<OrderDTO>> AddAsync(OrderCreateDTO orderCreateDTO, string identityId)
    {
        try
        {
            //Identity Id yi kullan
            var admin = await _adminRepository.GetByIdentityId(identityId);
            if (admin == null)
            {
                throw new Exception("Admin could not be determined from the IdentityId.");
            }

            var order = orderCreateDTO.Adapt<Order>();
            order.AdminId = admin.Id; // Set AdminId

            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangeAsync();

            if (orderCreateDTO.OrderDetails != null && orderCreateDTO.OrderDetails.Any())
            {
                foreach (var detailDto in orderCreateDTO.OrderDetails)
                {
                    var orderDetail = detailDto.Adapt<OrderDetail>(); //Mapster ile dönüştürme
                        orderDetail.OrderId = order.Id;
                    await _orderDetailRepository.AddAsync(orderDetail);
                }
                await _orderDetailRepository.SaveChangeAsync();
            }

            return new SuccessDataResult<OrderDTO>(order.Adapt<OrderDTO>(), Messages.ORDER_CREATED_SUCCESS);
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<OrderDTO>(orderCreateDTO.Adapt<OrderDTO>(), Messages.ORDER_CREATE_FAILED + ex.Message);
        }
    }


        /// <summary>
        ///  Sipariş bilgilerini günceller.
        /// </summary>
        /// <param name="orderUpdateDTO">Güncellenecek sipariş bilgileri</param>
        /// <returns></returns>
        public async Task<IDataResult<OrderDTO>> UpdateAsync(OrderUpdateDTO orderUpdateDTO)
        {
            try
            {
                // Mevcut siparişi getir
                var existingOrder = await _orderRepository.GetByIdAsync(orderUpdateDTO.Id);
                if (existingOrder == null)
                {
                    return new ErrorDataResult<OrderDTO>(Messages.ORDER_NOT_FOUND);
                }

                // Sipariş verilerini güncelle
                existingOrder = orderUpdateDTO.Adapt(existingOrder);
                await _orderRepository.UpdateAsync(existingOrder);
                await _orderRepository.SaveChangeAsync();

                // İlgili sipariş detaylarını güncelle
                foreach (var detailDto in orderUpdateDTO.OrderDetails)
                {
                    var existingDetail = await _orderDetailRepository.GetByIdAsync(existingOrder.Id);
                    if (existingDetail != null)
                    {
                        existingDetail.Quantity = detailDto.Quantity;
                        existingDetail.UnitPrice = detailDto.UnitPrice;
                        existingDetail.Discount = detailDto.Discount;
                        await _orderDetailRepository.UpdateAsync(existingDetail);
                    }
                    else
                    {
                        var newDetail = new OrderDetail
                        {
                            OrderId = existingOrder.Id,
                            ProductId = detailDto.ProductId,
                            Quantity = detailDto.Quantity,
                            UnitPrice = detailDto.UnitPrice,
                            Discount = detailDto.Discount
                        };
                        await _orderDetailRepository.AddAsync(newDetail);
                    }
                }
                await _orderDetailRepository.SaveChangeAsync();

                return new SuccessDataResult<OrderDTO>(existingOrder.Adapt<OrderDTO>(), Messages.ORDER_UPDATE_SUCCES);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<OrderDTO>(Messages.ORDER_UPDATE_FAILED + ex.Message);
            }
        }

        /// <summary>
        /// Belirtilen ID'li siparişi siler.
        /// </summary>
        /// <param name="orderId">Silinecek siparişin ID'si</param>
        /// <returns>Sipariş silme işleminin sonucu</returns>
        public async Task<IResult> DeleteAsync(Guid orderId)
        {
            try
            {
                var order = await _orderRepository.GetByIdAsync(orderId);
                if (order == null)
                {
                    return new ErrorResult(Messages.ORDER_NOT_FOUND);
                }

                await _orderRepository.DeleteAsync(order);
                await _orderRepository.SaveChangeAsync();

                return new SuccessResult(Messages.ORDER_DELETED_SUCCESS);
            }
            catch (Exception ex)
            {
                return new ErrorResult(Messages.ORDER_DELETE_FAILED + ex.Message);
            }
        }

        public Task<IDataResult<OrderDTO>> AddAsync(OrderCreateDTO orderCreateDTO)
        {
            throw new NotImplementedException();
        }
    }
}
