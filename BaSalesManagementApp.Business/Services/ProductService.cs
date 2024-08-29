using BaSalesManagementApp.Dtos.CategoryDTOs;
using BaSalesManagementApp.Dtos.ProductDTOs;
using BaSalesManagementApp.Dtos.StockDTOs;
using BaSalesManagementApp.Dtos.WarehouseDTOs;
using BaSalesManagementApp.Entites.DbSets;
using Microsoft.Identity.Client;

namespace BaSalesManagementApp.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IQrService _qrService;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IStockService _stockService;
        private readonly IOrderRepository _orderRepository;

        public ProductService(IProductRepository productRepository, IQrService qrService, ICategoryRepository categoryRepository, ICompanyRepository companyRepository, IStockService stockService, IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _qrService = qrService;
            _categoryRepository = categoryRepository;
            _companyRepository = companyRepository;
            _stockService = stockService;
            _orderRepository = orderRepository;
        }
        /// <summary>
        /// Yeni bir ürün oluşturur.
        /// </summary>
        /// <param name="productCreateDTO">Oluşturulacak ürünün özelliklerini içerir.</param>
        /// <returns></returns>
        public async Task<IDataResult<ProductDTO>> AddAsync(ProductCreateDTO productCreateDTO)
        {

            try
            {

                //var category = await _categoryRepository.GetByIdAsync(productCreateDTO.CategoryId);
                //if (category == null)
                //{
                //    return new ErrorDataResult<ProductDTO>(_localizer[Messages.CATEGORY_LIST_FAILED]);
                //}

                var company = await _companyRepository.GetByIdAsync(productCreateDTO.CompanyId);

                if (company == null)
                {
                    return new ErrorDataResult<ProductDTO>
                        (Messages.PRODUCT_CREATED_ERROR);
                }


                var product = productCreateDTO.Adapt<Product>();
                product.QRCode = _qrService.GenerateQrCode(product.Id.ToString());
                await _productRepository.AddAsync(product);
                await _productRepository.SaveChangeAsync();
                return new SuccessDataResult<ProductDTO>(product.Adapt<ProductDTO>(), Messages.PRODUCT_CREATED_SUCCESS);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<ProductDTO>(productCreateDTO.Adapt<ProductDTO>(), Messages.PRODUCT_CREATED_ERROR + " - " + ex.Message);
            }
        }

        /// <summary>
        /// Checks if the specified product is associated with any orders.
        /// </summary>
        /// <param name="productId">The unique identifier of the product.</param>
        /// <returns>A boolean value indicating whether the product is present in any order (true) or not (false).</returns>
        public async Task<bool> IsProductInOrderAsync(Guid productId)
        {
            return await _orderRepository.AnyAsync(o => o.ProductId == productId);
        }

        /// <summary>
        /// Id'si verilen ürünü siler ve bu ürünle ilişkili stok kayıtlarındaki miktarı sıfırlar.
        /// Eğer ürün bir siparişte mevcutsa, ürünü silmek yerine pasif hale getirir.
        /// </summary>
        /// <param name="productId">Silinecek olan ürünün Id değeri</param>
        /// <returns></returns>
        public async Task<IResult> DeleteAsync(Guid productId)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(productId);
                if (product == null)
                {
                    return new ErrorResult(Messages.PRODUCT_NOT_FOUND);
                }

                // Ürünün herhangi bir siparişte mevcut olup olmadığını kontrol edin
                var productInOrders = await _orderRepository.AnyAsync(o => o.ProductId == productId);
                if (productInOrders)
                {
                    // Ürün bir siparişte mevcut, bu yüzden silmek yerine pasif hale getiriyoruz
                    var result = await _productRepository.SetProductToPassiveAsync(productId);
                    if (!result.IsSuccess)
                    {
                        return result;
                    }
                    await _productRepository.SaveChangeAsync();
                    return new SuccessResult(Messages.PRODUCT_PASSIVED_SUCCESS);
                }
                else
                {
                    // Ürünü sil ve stok miktarlarını sıfırla
                    var stocks = await _stockService.GetAllAsync();
                    foreach (var stock in stocks.Data)
                    {
                        if (stock.ProductId == productId)
                        {
                            stock.Count = 0;
                            stock.ProductName = product.Name;
                            await _stockService.UpdateAsync(stock.Adapt<StockUpdateDTO>());
                        }
                    }

                    await _productRepository.DeleteAsync(product);
                }

                await _productRepository.SaveChangeAsync();
                return new SuccessResult(Messages.PRODUCT_DELETED_SUCCESS);
            }
            catch (Exception ex)
            {
                return new ErrorResult(Messages.PRODUCT_DELETED_ERROR + " - " + ex.Message);
            }
        }

        /// <summary>
        /// Tüm ürünleri liste olarak döndürür
        /// </summary>
        /// <returns></returns>
        public async Task<IDataResult<List<ProductListDTO>>> GetAllAsync()
        {
            try
            {
                var products = await _productRepository.GetAllAsync();
                var categories = await _categoryRepository.GetAllAsync();
                if (products == null)
                {
                    return new ErrorDataResult<List<ProductListDTO>>(new List<ProductListDTO>(), Messages.PRODUCT_LISTED_ERROR);
                }
                else if (products.Count() == 0)
                {
                    return new ErrorDataResult<List<ProductListDTO>>(new List<ProductListDTO>(), Messages.PRODUCT_LISTED_EMPTY);
                }

                var productListDTOs = products.Adapt<List<ProductListDTO>>();
                var companies = await _companyRepository.GetAllAsync();

                foreach (var productDTO in productListDTOs)
                {
                    var category = categories.FirstOrDefault(b => b.Id == productDTO.CategoryId);
                    productDTO.CategoryName = category?.Name;
                    var company = companies.FirstOrDefault(x => x.Id == productDTO.CompanyId);

                    if (company != null)
                    {
                        productDTO.CompanyName = company.Name;
                    }
                    else
                    {
                        productDTO.CompanyName = Messages.PRODUCT_COMPANY_DELETED;
                    }


                }

                return new SuccessDataResult<List<ProductListDTO>>(productListDTOs, Messages.PRODUCT_LISTED_SUCCESS);
            }
            catch (Exception)
            {

                return new ErrorDataResult<List<ProductListDTO>>(new List<ProductListDTO>(), Messages.PRODUCT_LISTED_ERROR);
            }

        }

        /// <summary>
        /// Verilen Id değerine sahip ürünü döndürür.
        /// </summary>
        /// <param name="productId">İstenen ürünün Id değeri</param>
        /// <returns></returns>
        public async Task<IDataResult<ProductDTO>> GetByIdAsync(Guid productId)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(productId);
                if (product == null)
                {
                    return new ErrorDataResult<ProductDTO>(Messages.PRODUCT_NOT_FOUND);
                }

                var productDTO = product.Adapt<ProductDTO>();
                var category = await _categoryRepository.GetByIdAsync(product.CategoryId);
                var company = await _companyRepository.GetByIdAsync(product.CompanyId);

                if (category != null && company != null)
                {
                    productDTO.CategoryName = category.Name;
                    productDTO.CompanyName = company.Name;
                }

                return new SuccessDataResult<ProductDTO>(product.Adapt<ProductDTO>(), Messages.PRODUCT_GET_SUCCESS);
            }
            catch (Exception)
            {
                return new ErrorDataResult<ProductDTO>(Messages.PRODUCT_NOT_FOUND);
            }

        }

        /// <summary>
        /// ID'sine karşılık gelen ürünü verilen özellikler ile günceller
        /// </summary>
        /// <param name="productUpdateDTO"></param>
        /// <returns></returns>
        public async Task<IResult> UpdateAsync(ProductUpdateDTO productUpdateDTO)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(productUpdateDTO.Id);
                if (product == null)
                {
                    return new ErrorResult(Messages.PRODUCT_NOT_FOUND);
                }

                var category = await _categoryRepository.GetByIdAsync(productUpdateDTO.CategoryId);
                if (category == null)
                {
                    return new ErrorResult(Messages.CATEGORY_LIST_FAILED);
                }


                var company = await _companyRepository.GetByIdAsync(productUpdateDTO.CompanyId);
                if (company == null)
                {
                    return new ErrorDataResult<ProductDTO>(Messages.COMPANY_GETBYID_ERROR);
                }

                product.Name = productUpdateDTO.Name;
                product.Price = productUpdateDTO.Price;
                product.CategoryId = productUpdateDTO.CategoryId;
                product.CompanyId = productUpdateDTO.CompanyId;

                await _productRepository.UpdateAsync(product);
                await _productRepository.SaveChangeAsync();

                return new SuccessResult(Messages.PRODUCT_UPDATED_SUCCESS);
            }
            catch (Exception)
            {
                return new ErrorResult(Messages.Warehouse_UPDATE_FAILED);
            }
        }
        /// <summary>
        /// Tüm ürünleri günceller
        /// </summary>
        /// <returns></returns>
        public async Task<IResult> UpdateAllProductsAsync()
        {
            try
            {
                var products = await _productRepository.GetAllAsync();
                foreach (var product in products)
                {
                    var updateDto = new ProductUpdateDTO
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Price = product.Price + 10,
                        CategoryId = product.CategoryId,
                        CompanyId = product.CompanyId
                    };
                    await UpdateAsync(updateDto);
                }

                return new SuccessResult("Tüm ürünler başarıyla güncellendi." + " " + DateTime.Now);
            }
            catch (Exception ex)
            {
                return new ErrorResult($"Tüm ürünler güncellenemedi: {ex.Message}" + " " + DateTime.Now);
            }
        }
    }
}
