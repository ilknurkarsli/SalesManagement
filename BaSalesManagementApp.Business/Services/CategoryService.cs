using BaSalesManagementApp.Dtos.CategoryDTOs;
using BaSalesManagementApp.Dtos.OrderDTOs;
using BaSalesManagementApp.Dtos.ProductDTOs;

namespace BaSalesManagementApp.Business.Services
{
    // CategoryService; kategorilerle ilgili CRUD işlemlerini gerçekleştirir.
    public class CategoryService : ICategoryService
    {
        // CategoryService kurucusu, ICategoryRepository bağımlılığını alır.
        private readonly ICategoryRepository _categoryRepository;
        /// <summary>
        /// Tüm Categorileri getirir.
        /// </summary>
        /// <returns>Tüm categori listesi</returns>
        public async Task<IDataResult<List<CategoryListDTO>>> GetAllAsync(string sortOrder)
        {
            try
            {
                var categories = await _categoryRepository.GetAllAsync();
                var categoryList = categories.Adapt<List<CategoryListDTO>>();

                if (categoryList == null || categoryList.Count == 0)
                {
                    return new ErrorDataResult<List<CategoryListDTO>>(categoryList, Messages.CATEGORY_LIST_EMPTY);
                }

                switch (sortOrder.ToLower())
                {
                    case "date":
                        categoryList = categoryList.OrderByDescending(c => c.CreatedDate).ToList();
                        break;
                    case "datedesc":
                        categoryList = categoryList.OrderBy(c => c.CreatedDate).ToList();
                        break;
                    case "alphabetical":
                        categoryList = categoryList.OrderBy(c => c.Name).ToList();
                        break;
                    case "alphabeticaldesc":
                        categoryList = categoryList.OrderByDescending(c => c.Name).ToList();
                        break;
                        //default:
                        //    categoryList = categoryList.OrderBy(c => c.Name).ToList();
                        //    break;
                }

                return new SuccessDataResult<List<CategoryListDTO>>(categoryList, Messages.CATEGORY_LISTED_SUCCESS);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<CategoryListDTO>>(Messages.CATEGORY_LIST_FAILED + ex.Message);
            }
        }
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// Yeni bir kategori ekler.
        /// </summary>
        /// <param name="categoryCreateDTO">Eklenmek istenen kategoriyle ilgili bilgileri içeren veri transfer nesnesi.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda eklenen kategorileri verilerini içerir.</returns>
        public async Task<IDataResult<CategoryDTO>> AddAsync(CategoryCreateDTO categoryCreateDTO)
        {
            try
            {
                var category = categoryCreateDTO.Adapt<Category>();

                await _categoryRepository.AddAsync(category);

                await _categoryRepository.SaveChangeAsync();

                return new SuccessDataResult<CategoryDTO>(category.Adapt<CategoryDTO>(), Messages.CATEGORY_CREATED_SUCCESS);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<CategoryDTO>(Messages.CATEGORY_CREATE_FAILED + ex.Message);
            }
        }

        /// <summary>
        /// Benzersiz kimliğiyle bir kategori alır.
        /// </summary>
        /// <param name="categoryId">Alınmak istenen kategorinin benzersiz kimliği.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda kategori verilerini içerir, bulunamazsa null döner.</returns>
        public async Task<IResult> DeleteAsync(Guid categoryId)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(categoryId);
                
                if (category == null)
                {
                    return new ErrorResult(Messages.CATEGORY_NOT_FOUND);
                }

                await _categoryRepository.DeleteAsync(category);
                await _categoryRepository.SaveChangeAsync();

                return new SuccessResult(Messages.CATEGORY_DELETED_SUCCESS);
            }
            catch (Exception ex)
            {
                return new ErrorResult(Messages.CATEGORY_DELETE_FAILED + ex.Message);
            }
        }

        /// <summary>
        /// Tüm kategorileri alır.
        /// </summary>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda tüm kategorilerin listesini içerir.</returns>
        public async Task<IDataResult<List<CategoryListDTO>>> GetAllAsync()
        {
            try
            {
                var categories = await _categoryRepository.GetAllAsync();
                var categoryListDTOs = categories.Adapt<List<CategoryListDTO>>() ?? new List<CategoryListDTO>();

                if (categories == null || categories.Count() == 0)
                {
                    return new ErrorDataResult<List<CategoryListDTO>>(categoryListDTOs, Messages.CATEGORY_LIST_EMPTY);
                }

                return new SuccessDataResult<List<CategoryListDTO>>(categoryListDTOs, Messages.CATEGORY_LISTED_SUCCESS);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<CategoryListDTO>>(Messages.CATEGORY_LIST_FAILED + ex.Message);
            }
        }

        /// <summary>
        /// Benzersiz kimliğiyle bir kategori alır.
        /// </summary>
        /// <param name="categoryId">Alınmak istenen kategorinin benzersiz kimliği.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda kategori verilerini içerir, bulunamazsa null döner.</returns>
        public async Task<IDataResult<CategoryDTO>> GetByIdAsync(Guid categoryId)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(categoryId);
                if (category == null)
                {
                    return new ErrorDataResult<CategoryDTO>(Messages.CATEGORY_NOT_FOUND);
                }

                return new SuccessDataResult<CategoryDTO>(category.Adapt<CategoryDTO>(), Messages.CATEGORY_FOUND_SUCCESS);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<CategoryDTO>(Messages.CATEGORY_GET_FAILED + ex.Message);
            }
        }

        /// <summary>
        /// Bir kategoryi günceller.
        /// </summary>
        /// <param name="categoryUpdateDTO">Güncellenmiş kategoriyle ilgili bilgileri içeren veri transfer nesnesi.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda güncellenmiş kategori verilerini içerir.</returns>
        public async Task<IDataResult<CategoryDTO>> UpdateAsync(CategoryUpdateDTO categoryUpdateDTO)
        {
            try
            {
                var oldCategory = await _categoryRepository.GetByIdAsync(categoryUpdateDTO.Id);

                if (oldCategory == null)
                {
                    return new ErrorDataResult<CategoryDTO>(Messages.CATEGORY_NOT_FOUND);
                }

                var updatedCategory = categoryUpdateDTO.Adapt(oldCategory);

                await _categoryRepository.UpdateAsync(updatedCategory);

                await _categoryRepository.SaveChangeAsync();

                return new SuccessDataResult<CategoryDTO>(updatedCategory.Adapt<CategoryDTO>(), Messages.CATEGORY_UPDATED_SUCCESS);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<CategoryDTO>(Messages.CATEGORY_UPDATED_FAILED + ex.Message);
            }

        }
    }
}
