using BaSalesManagementApp.Dtos.StockDTOs;

namespace BaSalesManagementApp.Business.Services
{
    /// <summary>
    /// StockService sınıfı, stoklarla ilgili CRUD işlemlerini gerçekleştirir.
    /// </summary>
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;
        private readonly IProductRepository _productRepository;
        public StockService(IStockRepository stockRepository, IProductRepository productRepository)
        {
            _stockRepository = stockRepository;
            _productRepository = productRepository;
        }

        /// <summary>
        /// Yeni bir stok oluşturur.
        /// </summary>
        /// <param name="stockCreateDTO">Oluşturulacak stok bilgileri</param>
        /// <returns>Stok oluşturma işlemi sonucu</returns>
        public async Task<IDataResult<StockDTO>> AddAsync(StockCreateDTO stockCreateDTO)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(stockCreateDTO.ProductId);
                if (product == null)
                {
                    return new ErrorDataResult<StockDTO>(Messages.PRODUCT_LISTED_EMPTY);
                }
                var stock = stockCreateDTO.Adapt<Stock>();
                await _stockRepository.AddAsync(stock);
                await _stockRepository.SaveChangeAsync();
                return new SuccessDataResult<StockDTO>(stock.Adapt<StockDTO>(), Messages.STOCK_CREATED_SUCCESS);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<StockDTO>(stockCreateDTO.Adapt<StockDTO>(), Messages.STOCK_CREATE_FAILED + ex.Message);
            }
        }

        /// <summary>
        /// Belirtilen ID'li stoğu siler.
        /// </summary>
        /// <param name="stockId">Silinecek stok ID'si</param>
        /// <returns>Stok silme işlemi sonucu</returns>
        public async Task<IResult> DeleteAsync(Guid stockId)
        {
            try
            {
                var stock = await _stockRepository.GetByIdAsync(stockId);
                if (stock == null)
                {
                    return new ErrorResult(Messages.STOCK_NOT_FOUND);
                }
                await _stockRepository.DeleteAsync(stock);
                await _stockRepository.SaveChangeAsync();
                return new SuccessResult(Messages.STOCK_DELETED_SUCCESS);
            }
            catch (Exception ex)
            {
                return new SuccessResult(Messages.STOCK_DELETE_FAILED + ex.Message);
            }
        }

        /// <summary>
        /// Tüm stokları getirir.
        /// </summary>
        /// <returns>Tüm stok listesi</returns>
        public async Task<IDataResult<List<StockListDTO>>> GetAllAsync(string sortOrder)
        {
            try
            {
                var stocks = await _stockRepository.GetAllAsync();
                var products = await _productRepository.GetAllAsync();
                var stockList = stocks.Adapt<List<StockListDTO>>();

                if (stockList == null || stockList.Count == 0)
                {
                    return new ErrorDataResult<List<StockListDTO>>(stockList, Messages.STOCK_LIST_EMPTY);
                }
                switch (sortOrder.ToLower())
                {
                    case "date":
                        stockList = stockList.OrderByDescending(s => s.CreatedDate).ToList(); 
                        break;
                    case "datedesc":
                        stockList = stockList.OrderBy(s => s.CreatedDate).ToList();
                        break;
                    case "alphabetical":
                        stockList = stockList.OrderBy(s => s.ProductName).ToList();
                        break;
                    case "alphabeticaldesc":
                        stockList = stockList.OrderByDescending(s => s.ProductName).ToList();
                        break;
                }

                foreach (var stockDTO in stockList)
                {
                    var product = products.FirstOrDefault(p => p.Id == stockDTO.ProductId);
                    stockDTO.ProductName = product?.Name;
                }

                return new SuccessDataResult<List<StockListDTO>>(stockList, Messages.STOCK_LISTED_SUCCESS);
            }
            catch (Exception)
            {
                return new ErrorDataResult<List<StockListDTO>>(new List<StockListDTO>(), Messages.STOCK_LIST_FAILED);
            }
        }

        public async Task<IDataResult<List<StockListDTO>>> GetAllAsync()
        {
            try
            {
                var stocks = await _stockRepository.GetAllAsync();
                var products = await _productRepository.GetAllAsync();
                var stockList = stocks.Adapt<List<StockListDTO>>();

                if (stockList == null || stockList.Count == 0)
                {
                    return new ErrorDataResult<List<StockListDTO>>(stockList, Messages.STOCK_LIST_EMPTY);
                }
                

                foreach (var stockDTO in stockList)
                {
                    var product = products.FirstOrDefault(p => p.Id == stockDTO.ProductId);
                    stockDTO.ProductName = product?.Name;
                }

                return new SuccessDataResult<List<StockListDTO>>(stockList, Messages.STOCK_LISTED_SUCCESS);
            }
            catch (Exception)
            {
                return new ErrorDataResult<List<StockListDTO>>(new List<StockListDTO>(), Messages.STOCK_LIST_FAILED);
            }
        }

        /// <summary>
        /// Belirtilen ID'li stok getirilir.
        /// </summary>
        /// <param name="stockId">Getirilecek stok ID'si</param>
        /// <returns>Belirtilen ID'li stok verileri</returns>
        public async Task<IDataResult<StockDTO>> GetByIdAsync(Guid stockId)
        {
            try
            {
                var stock = await _stockRepository.GetByIdAsync(stockId);
                if (stock == null)
                {
                    return new ErrorDataResult<StockDTO>(Messages.STOCK_NOT_FOUND);
                }
                //ProductName ekle
                var stockDTO = stock.Adapt<StockDTO>();
                var product = await _productRepository.GetByIdAsync(stock.ProductId);
                if (product == null)
                {
                    stockDTO.ProductName = product.Name;
                }
                return new SuccessDataResult<StockDTO>(stock.Adapt<StockDTO>(), Messages.STOCK_FOUND_SUCCESS);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<StockDTO>(Messages.STOCK_GET_FAILED + ex.Message);
            }
        }

        /// <summary>
        ///  Stok bilgilerini günceller.
        /// </summary>
        /// <param name="stockUpdateDTO">Güncellenecek stok bilgileri</param>
        /// <returns></returns>
        public async Task<IDataResult<StockDTO>> UpdateAsync(StockUpdateDTO stockUpdateDTO)
        {
            try
            {
                var stockOnHand = await _stockRepository.GetByIdAsync(stockUpdateDTO.Id);
                if (stockOnHand == null)
                {
                    return new ErrorDataResult<StockDTO>(Messages.STOCK_NOT_FOUND);
                }
                var product = await _productRepository.GetByIdAsync(stockUpdateDTO.ProductId);
                if (product == null)
                {
                    return new ErrorDataResult<StockDTO>(Messages.PRODUCT_LISTED_ERROR);
                }

                stockOnHand = stockUpdateDTO.Adapt(stockOnHand);
                await _stockRepository.UpdateAsync(stockOnHand);
                await _stockRepository.SaveChangeAsync();
                return new SuccessDataResult<StockDTO>(stockOnHand.Adapt<StockDTO>(), Messages.STOCK_UPDATE_SUCCESS);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<StockDTO>(Messages.STOCK_UPDATE_FAILED + ex.Message);
            }
        }
    }
}
