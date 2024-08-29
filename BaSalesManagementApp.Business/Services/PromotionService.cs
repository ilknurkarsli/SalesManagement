using BaSalesManagementApp.Dtos.PromotionDTOs;

namespace BaSalesManagementApp.Business.Services
{
	/// <summary>
	/// PromotionService sınıfı, promosyonlar ile ilgili CRUD işlemlerini gerçekleştirir.
	/// </summary>
	public class PromotionService : IPromotionService
	{
		private readonly IPromotionRepository _promotionRepository;
		private readonly IProductRepository _productRepository;
		private readonly ICompanyRepository _companyRepository;

		/// <summary>
		/// PromotionService kurucusu (constructor method), IPromotionRepository bağımlılığını alır.
		/// </summary>
		/// <param name="promotionRepository">promosyon deposu</param>
		/// <param name="productRepository">ürün deposu</param>
		/// <param name="localizer">yerelleştirme servisi</param>
		public PromotionService(IPromotionRepository promotionRepository, IProductRepository productRepository, ICompanyRepository companyRepository)
		{
			_promotionRepository = promotionRepository;
			_productRepository = productRepository;
			_companyRepository = companyRepository;
		}

		/// <summary>
		/// Yeni bir promosyon oluşturur.
		/// </summary>
		/// <param name="promotionCreateDTO">oluşturulacak promosyon bilgilerini taşıyan veri transfer nesnesi</param>
		/// <returns>promosyon oluşturma işleminin sonucu</returns>
		public async Task<IDataResult<PromotionDTO>> AddAsync(PromotionCreateDTO promotionCreateDTO)
		{
			try
			{
				var product = await _productRepository.GetByIdAsync(promotionCreateDTO.ProductId);
				var company = await _companyRepository.GetByIdAsync(promotionCreateDTO.CompanyId);

				if (product == null || company == null)
				{
					return new ErrorDataResult<PromotionDTO>
						(Messages.PROMOTION_CREATE_ERROR);
				}

				var promotion = promotionCreateDTO.Adapt<Promotion>();
				await _promotionRepository.AddAsync(promotion);
				await _promotionRepository.SaveChangeAsync();

				return new SuccessDataResult<PromotionDTO>
					(promotion.Adapt<PromotionDTO>(), Messages.PROMOTION_CREATE_SUCCESS);
			}
			catch (Exception ex)
			{
				return new ErrorDataResult<PromotionDTO>
					(Messages.PROMOTION_CREATE_ERROR + " " + ex.Message);
			}
		}

		/// <summary>
		/// Belirtilen ID'li promosyonu siler.
		/// </summary>
		/// <param name="promotionId">Silinecek promosyonun ID'si</param>
		/// <returns>Promosyon silme işleminin sonucu</returns>
		public async Task<IResult> DeleteAsync(Guid promotionId)
		{
			try
			{
				var promotion = await _promotionRepository.GetByIdAsync(promotionId);
				if (promotion == null)
				{
					return new ErrorResult(Messages.PROMOTION_NOT_FOUND);
				}
				await _promotionRepository.DeleteAsync(promotion);
				await _promotionRepository.SaveChangeAsync();

				return new SuccessResult(Messages.PROMOTION_DELETE_SUCCESS);
			}
			catch (Exception ex)
			{
				return new ErrorResult(Messages.PROMOTION_DELETE_ERROR + " " + ex.Message);
			}
		}

        /// <summary>
        /// Tüm promosyonları getirir.
        /// </summary>
        /// <returns>Tüm promosyonların listesini döndürme işleminin sonucu</returns>
        public async Task<IDataResult<List<PromotionListDTO>>> GetAllAsync()
        {
            try
            {
                var promotions = await _promotionRepository.GetAllAsync();
                if (promotions == null || !promotions.Any())
                {
                    return new ErrorDataResult<List<PromotionListDTO>>(new List<PromotionListDTO>(),Messages.PROMOTION_LISTED_NOTFOUND);
                }

                var products = await _productRepository.GetAllAsync();
                var companies = await _companyRepository.GetAllAsync();
                var promotionListDTOs = promotions.Adapt<List<PromotionListDTO>>();


                var validPromotions = new List<PromotionListDTO>();

                foreach (var promotion in promotionListDTOs)
                {
                    var product = products.FirstOrDefault(x => x.Id == promotion.ProductId);
                    var company = companies.FirstOrDefault(x => x.Id == promotion.CompanyId);

                    if (product != null && company != null)
                    {
                        promotion.ProductName = product.Name;
                        promotion.CompanyName = company.Name;
                        validPromotions.Add(promotion);
                    }
                }

                return new SuccessDataResult<List<PromotionListDTO>>(validPromotions, Messages.PROMOTION_LISTED_SUCCESS);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<PromotionListDTO>>(null, Messages.PROMOTION_LISTED_ERROR + " " + ex.Message);
            }
        }


        /// <summary>
        /// Belirtilen ID'li promosyonu getirir.
        /// </summary>
        /// <param name="promotionId">Getirilecek promosyonun ID'si</param>
        /// <returns>Belirtilen ID'li promosyonun verileri getirme isleminin sonucu</returns>
        public async Task<IDataResult<PromotionDTO>> GetByIdAsync(Guid promotionId)
		{
			try
			{
				var promotion = await _promotionRepository.GetByIdAsync(promotionId);
				if (promotion == null)
				{
					return new ErrorDataResult<PromotionDTO>(Messages.PROMOTION_NOT_FOUND);
				}

				var promotionDTO = promotion.Adapt<PromotionDTO>();
				var product = await _productRepository.GetByIdAsync(promotion.ProductId);
				var company = await _companyRepository.GetByIdAsync(promotion.CompanyId);

				if (product == null)
				{
					promotionDTO.ProductName = Messages.PROMOTION_PRODUCT_DELETED;
				}
				else
				{
					promotionDTO.ProductName = product.Name;
				}

				if (company == null)
				{
					promotionDTO.CompanyName = Messages.PROMOTION_COMPANY_DELETED;
				}
				else
				{
					promotionDTO.CompanyName = company.Name;
				}

				if (product != null && company != null)
				{
					promotionDTO.ProductName = product.Name;
					promotionDTO.CompanyName = company.Name;
				}

				return new SuccessDataResult<PromotionDTO>(promotion.Adapt<PromotionDTO>(), Messages.PROMOTION_GETBYID_SUCCESS);
			}
			catch (Exception ex)
			{
				return new ErrorDataResult<PromotionDTO>(Messages.PROMOTION_GETBYID_ERROR + " " + ex.Message);
			}
		}

		/// <summary>
		/// Belirtilen ID'li promosyonu günceller.
		/// </summary>
		/// <param name="promotionUpdateDTO">Güncellenecek promosyon bilgilerini taşıyan veri transfer nesnesi</param>
		/// <returns>Güncellenme işleminin sonucu</returns>
		public async Task<IDataResult<PromotionDTO>> UpdateAsync(PromotionUpdateDTO promotionUpdateDTO)
		{
			try
			{
				var updatingPromotion = await _promotionRepository.GetByIdAsync(promotionUpdateDTO.Id);
				if (updatingPromotion == null)
				{
					return new ErrorDataResult<PromotionDTO>(Messages.PROMOTION_NOT_FOUND);
				}

				var product = await _productRepository.GetByIdAsync(promotionUpdateDTO.ProductId);
				if (product == null)
				{
					return new ErrorDataResult<PromotionDTO>(Messages.PRODUCT_NOT_FOUND);
				}

				var company = await _companyRepository.GetByIdAsync(promotionUpdateDTO.CompanyId);
				if (company == null)
				{
					return new ErrorDataResult<PromotionDTO>(Messages.COMPANY_GETBYID_ERROR);
				}

				promotionUpdateDTO.Adapt(updatingPromotion);
				await _promotionRepository.UpdateAsync(updatingPromotion);
				await _promotionRepository.SaveChangeAsync();

				return new SuccessDataResult<PromotionDTO>(promotionUpdateDTO.Adapt<PromotionDTO>(), Messages.PROMOTION_UPDATE_SUCCESS);
			}
			catch (Exception ex)
			{
				return new ErrorDataResult<PromotionDTO>(Messages.PROMOTION_UPDATE_ERROR + " " + ex.Message);
			}
		}

		/// <summary>
		/// Belirtilen Job ID ile ilgili promosyon güncellemelerini bildirir.
		/// </summary>
		/// <param name="jobId">Job ID'si</param>
		/// <returns>Asenkron bir görev döndürür</returns>
		public async Task NotifyPromotionUpdatesWithJobId(string jobId)
		{
			var promotions = await _promotionRepository.GetAllAsync();
			if (promotions == null || !promotions.Any())
			{
				Console.WriteLine("Süresi dolan promosyon bulunamadı." + " " + DateTime.Now);
				return;
			}

			var products = await _productRepository.GetAllAsync();
			foreach (var promotion in promotions)
			{
				var product = products.FirstOrDefault(x => x.Id == promotion.ProductId);
				var productName = product != null ? product.Name : "Ürün bulunamadı";

				if (promotion.EndDate < DateTime.Now)
				{
					Console.WriteLine($"Süresi dolan promosyon: {productName} (Job ID: {jobId})" + " " + DateTime.Now);
				}
				else
				{
					Console.WriteLine($"Süresi dolmayan promosyon: {productName} (Job ID: {jobId})" + " " + DateTime.Now);
				}
			}
		}
        /// <summary>
        /// Tüm promosyonları belirli bir sıralama düzenine göre asenkron olarak getirir.
        /// Promosyonlar arasında ilgili ürün ve şirket bilgilerini ekler.
        /// </summary>
        /// <param name="sortOrder">Promosyonların sıralama düzenini belirtir (ör. "date", "datedesc", "alphabetical", "alphabeticaldesc").</param>
        /// <returns>Promosyonların listesi ve işlem sonucu hakkında bilgi içeren bir IDataResult nesnesi döner.
        /// Başarılıysa, promosyonların liste olarak döndüğü bir SuccessDataResult döner.
        /// Başarısızsa, hata mesajı içeren bir ErrorDataResult döner.</returns>
        public async Task<IDataResult<List<PromotionListDTO>>> GetAllAsync(string sortOrder)
        {
            try
            {
                var promotions = await _promotionRepository.GetAllAsync();
                if (promotions == null || !promotions.Any())
                {
                    return new ErrorDataResult<List<PromotionListDTO>>(new List<PromotionListDTO>(), Messages.PROMOTION_LISTED_NOTFOUND);
                }

                var products = await _productRepository.GetAllAsync();
                var companies = await _companyRepository.GetAllAsync();
                var promotionListDTOs = promotions.Adapt<List<PromotionListDTO>>();

                var validPromotions = new List<PromotionListDTO>();

                foreach (var promotion in promotionListDTOs)
                {
                    var product = products.FirstOrDefault(x => x.Id == promotion.ProductId);
                    var company = companies.FirstOrDefault(x => x.Id == promotion.CompanyId);

                   

                    if (product != null && company != null)
                    {
                        promotion.ProductName = product.Name;
                        promotion.CompanyName = company.Name;
                        validPromotions.Add(promotion);
                    }

                }
                if (validPromotions == null || validPromotions.Count == 0)
                {
                    return new ErrorDataResult<List<PromotionListDTO>>(validPromotions, Messages.PROMOTION_LISTED_ERROR);
                }

                switch (sortOrder.ToLower())
                {
                    case "date":
                        promotionListDTOs = promotionListDTOs.OrderByDescending(s => s.StartDate).ToList();
                        break;
                    case "datedesc":
                        promotionListDTOs = promotionListDTOs.OrderBy(s => s.StartDate).ToList();
                        break;
                    case "alphabetical":
                        promotionListDTOs = promotionListDTOs.OrderBy(s => s.ProductName).ToList();
                        break;
                    case "alphabeticaldesc":
                        promotionListDTOs = promotionListDTOs.OrderByDescending(s => s.ProductName).ToList();
                        break;
                }


                return new SuccessDataResult<List<PromotionListDTO>>(validPromotions, Messages.PROMOTION_LISTED_SUCCESS);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<PromotionListDTO>>(new List<PromotionListDTO>(), Messages.PROMOTION_LISTED_ERROR + " " + ex.Message);
            }
        }


    }
}
