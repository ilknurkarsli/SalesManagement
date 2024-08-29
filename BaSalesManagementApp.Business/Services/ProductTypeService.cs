using BaSalesManagementApp.Dtos.ProductTypeDtos;

namespace BaSalesManagementApp.Business.Services
{
    /// <summary>
    /// ProductTypeService sınıfı ürün tipleri ile ilgili işlemleri gerçekleştirir.
    /// </summary>
    public class ProductTypeService : IStockTypeService
    {
        private readonly IStockTypeRepository productTypeRepository;
        public ProductTypeService(IStockTypeRepository productTypeRepository)
        {
            this.productTypeRepository = productTypeRepository;
        }

        /// <summary>
        /// Yeni bir ürün tipi ekler.
        /// </summary>
        /// <param name="productTypeAddDto">Eklenmek istenen ürün tipi ile ilgili verileri içeren veri transfer nesnesi.</param>
        /// <returns>İşlem sonucunda eklenen ürün tipi verilerini geri döndürür.</returns>
        public async Task<IDataResult<StockTypeDto>> AddAsync(StockTypeAddDto productTypeAddDto)
        {
            try
            {
                var productType = productTypeAddDto.Adapt<StockType>();
                await productTypeRepository.AddAsync(productType);
                await productTypeRepository.SaveChangeAsync();
                return new SuccessDataResult<StockTypeDto>(productType.Adapt<StockTypeDto>(), Messages.PRODUCTTYPE_ADD_SUCCESS);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<StockTypeDto>(productTypeAddDto.Adapt<StockTypeDto>(), Messages.PRODUCTTYPE_ADD_UNSUCCESS + " - " + exception.Message);
            }
        }

        /// <summary>
        /// Benzer kimliği ile bir ürün tipini siler.
        /// </summary>
        /// <param name="productTypeId">Silinmek istenen ürün tipinin benzersiz kimliği.</param>
        /// <returns>İşlem sonucunda silme işleminin başarılı olup olmadığı bilgisini döner.</returns>
        public async Task<IResult> DeleteAsync(Guid productTypeId)
        {
            try
            {
                var productType = await productTypeRepository.GetByIdAsync(productTypeId);
                if (productType is null) return new ErrorResult(Messages.PRODUCTTYPE_GETBYID_UNSUCCESS);

                await productTypeRepository.DeleteAsync(productType);
                await productTypeRepository.SaveChangeAsync();
                return new SuccessResult(Messages.PRODUCTTYPE_DELETE_SUCCESS);
            }
            catch (Exception exception)
            {
                return new ErrorResult(Messages.PRODUCTTYPE_DELETE_UNSUCCESS + " - " + exception.Message);
            }
        }

        /// <summary>
        /// Bir ürün tipini günceller.
        /// </summary>
        /// <param name="productTypeUpdateDto">Güncellenmiş ürün tipi ile ilgili verileri döner.</param>
        /// <returns>İşlem sonucunda güncellenen ürün tipi verilerini döndürür.</returns>
        public async Task<IDataResult<StockTypeDto>> UpdateAsync(StockTypeUpdateDto productTypeUpdateDto)
        {
            try
            {
                var existingProductType = await productTypeRepository.GetByIdAsync(productTypeUpdateDto.Id);
                if (existingProductType is null) return new ErrorDataResult<StockTypeDto>(Messages.PRODUCTTYPE_GETBYID_UNSUCCESS);

                var updatedProductType = productTypeUpdateDto.Adapt(existingProductType);
                await productTypeRepository.UpdateAsync(updatedProductType);
                await productTypeRepository.SaveChangeAsync();
                return new SuccessDataResult<StockTypeDto>(updatedProductType.Adapt<StockTypeDto>(), Messages.PRODUCTTYPE_UPDATE_SUCCESS);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<StockTypeDto>(Messages.PRODUCTTYPE_UPDATE_UNSUCCESS + " - " + exception.Message);
            }
        }

        /// <summary>
        /// Benzersiz kimliği ile bir ürün tipini getirir.
        /// </summary>
        /// <param name="productTypeId">Getirilmek istenen ürün tipinin benzersiz kimliği.</param>
        /// <returns>İşlem sonucunda varsa eğer ürün tipini döndürür yoksa null döner.</returns>
        public async Task<IDataResult<StockTypeDto>> GetByAsync(Guid productTypeId)
        {
            try
            {
                var producType = await productTypeRepository.GetByIdAsync(productTypeId);
                if (producType is null) return new ErrorDataResult<StockTypeDto>(Messages.PRODUCTTYPE_GETBYID_UNSUCCESS);

                return new SuccessDataResult<StockTypeDto>(producType.Adapt<StockTypeDto>(), Messages.PRODUCTTYPE_GETBYID_SUCCESS);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<StockTypeDto>(Messages.PRODUCTTYPE_GETBYID_UNSUCCESS + " - " + exception.Message);
            }
        }

        /// <summary>
        /// Tüm ürün tiplerini getirir.
        /// </summary>
        /// <returns>İşlem sonucunda varsa eğer tüm ürün tiplerini getirir yoksa null döner.</returns>
        public async Task<IDataResult<IEnumerable<StockTypeListDto>>> GetAllAsync()
        {
            try
            {
                var productTypes = await productTypeRepository.GetAllAsync();
                var productTypeList = productTypes.Adapt<IEnumerable<StockTypeListDto>>() ?? new List<StockTypeListDto>();
                if (productTypeList is null || productTypeList.Count() == 0) return new ErrorDataResult<IEnumerable<StockTypeListDto>>(productTypeList, Messages.PRODUCTTYPE_LISTED_NOTFOUND);

                return new SuccessDataResult<IEnumerable<StockTypeListDto>>(productTypes.Adapt<IEnumerable<StockTypeListDto>>(), Messages.PRODUCTTYPE_LISTED_SUCCESS);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<IEnumerable<StockTypeListDto>>(new List<StockTypeListDto>(), Messages.PRODUCTTYPE_LISTED_UNSUCCESS + " - " + exception.Message);
            }
        }
    }
}