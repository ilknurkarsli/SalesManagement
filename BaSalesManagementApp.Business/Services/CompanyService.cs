using BaSalesManagementApp.Business.Interfaces;
using BaSalesManagementApp.Dtos.CompanyDTOs;

namespace BaSalesManagementApp.Business.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IBranchService _branchService;
        public CompanyService(ICompanyRepository companyRepository, IBranchService branchService)
        {
            _companyRepository = companyRepository;
            _branchService = branchService;
        }

        //Yeni bir firma ekler ve işlem başarılıysa eklenen firmayı döndürür. Eğer bir hata oluşursa, uygun bir hata mesajıyla birlikte hata durumunu döndürür.
        public async Task<IDataResult<CompanyDTO>> AddAsync(CompanyCreateDTO companyCreateDTO)
        {          
            try
            {
                var newBranch = companyCreateDTO.Adapt<Company>();

                await _companyRepository.AddAsync(newBranch);
                await _companyRepository.SaveChangeAsync();

                return new SuccessDataResult<CompanyDTO>(newBranch.Adapt<CompanyDTO>(), Messages.COMPANY_ADD_SUCCESS);
            }
            catch (Exception)
            {
                return new ErrorDataResult<CompanyDTO>(Messages.COMPANY_ADD_ERROR);
            }            
        }

        //Belirtilen bir firmayı siler ve işlem başarılıysa başarılı bir mesaj döndürür.Herhangi bir hata oluşursa uygun bir hata mesajıyla birlikte hata durumunu döndürür.

        public async Task<IResult> DeleteAsync(Guid companyId)
        {
            try
            {
                var deletingCompany = await _companyRepository.GetByIdAsync(companyId);
                var branches = await _branchService.GetBranchesByCompanyIdAsync(companyId);

                foreach (var branch in branches)
                {
                    await _branchService.DeleteAsync(branch.Id);
                }


                await _companyRepository.DeleteAsync(deletingCompany);
                await _companyRepository.SaveChangeAsync();

                return new SuccessResult(Messages.COMPANY_DELETE_SUCCESS);
            }
            catch (Exception)
            {
                return new ErrorResult(Messages.COMPANY_DELETE_ERROR);
            }            
        }

        //Tüm firmaları getirir ve işlem başarılıysa firma listesini döndürür.Eğer hiç şube bulunamazsa uygun bir mesajla birlikte hata durumunu döndürür.

        public async Task<IDataResult<List<CompanyListDTO>>> GetAllAsync()
        {            
            try
            {
                IEnumerable<Company> companies;
                companies = await _companyRepository.GetAllAsync();

                if (companies.Count() == 0)
                {
                    return new ErrorDataResult<List<CompanyListDTO>>(new List<CompanyListDTO>(), Messages.COMPANY_LISTED_NOTFOUND);
                }

                return new SuccessDataResult<List<CompanyListDTO>>(companies.Adapt<List<CompanyListDTO>>(),Messages.COMPANY_LISTED_SUCCESS);
            }
            catch (Exception)
            {
                return new ErrorDataResult<List<CompanyListDTO>>(new List<CompanyListDTO>(), Messages.COMPANY_LISTED_ERROR);
            }            
        }

        //Belirli bir firma kimliğine göre firmayı getirir.Firma bulunamazsa uygun bir hata mesajıyla birlikte hata durumunu döndürür.

        public async Task<IDataResult<CompanyDTO>> GetByIdAsync(Guid companyId)
        {
            try
            {
                var company = await _companyRepository.GetByIdAsync(companyId);

                if (company == null)
                {
                    return new ErrorDataResult<CompanyDTO>(Messages.COMPANY_GETBYID_ERROR);
                }

                return new SuccessDataResult<CompanyDTO>(company.Adapt<CompanyDTO>(), Messages.COMPANY_GETBYID_SUCCESS);
            }
            catch
            {
                return new ErrorDataResult<CompanyDTO>(Messages.COMPANY_GETBYID_ERROR);
            }
        }

        //Belirli bir firma kimliğine göre firma bilgilerini günceller.Güncelleme başarılıysa güncellenen firma bilgilerini döndürür.Herhangi bir hata oluşursa uygun bir hata mesajıyla birlikte hata durumunu döndürür.

        public async Task<IDataResult<CompanyDTO>> UpdateAsync(CompanyUpdateDTO companyUpdateDTO)
        {      
            try
            {
                var updatingCompany = await _companyRepository.GetByIdAsync(companyUpdateDTO.Id);

                if (updatingCompany == null)
                {
                    return new ErrorDataResult<CompanyDTO>(Messages.COMPANY_GETBYID_ERROR);
                }

                await _companyRepository.UpdateAsync(companyUpdateDTO.Adapt(updatingCompany));
                await _companyRepository.SaveChangeAsync();

                return new SuccessDataResult<CompanyDTO>(updatingCompany.Adapt<CompanyDTO>(), Messages.COMPANY_UPDATE_SUCCESS);
            }
            catch (Exception)
            {
                return new ErrorDataResult<CompanyDTO>(Messages.COMPANY_UPDATE_ERROR);
            }            
        }
    }
}
