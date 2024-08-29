
using BaSalesManagementApp.Dtos.BranchDTOs;
using BaSalesManagementApp.Dtos.WarehouseDTOs;

namespace BaSalesManagementApp.Business.Services
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository _branchRepository;
        private readonly ICompanyRepository _companyRepository;

        public BranchService(IBranchRepository branchRepository,ICompanyRepository companyRepository)
        {
            _branchRepository = branchRepository;
            _companyRepository = companyRepository;
        }

        //Yeni bir şube ekler ve işlem başarılıysa eklenen şubeyi döndürür. Eğer bir hata oluşursa, uygun bir hata mesajıyla birlikte hata durumunu döndürür.
        public async Task<IDataResult<BranchDTO>> AddAsync(BranchCreateDTO branchCreateDTO)
        {              
            try
            {
                var newBranch = branchCreateDTO.Adapt<Branch>();

                await _branchRepository.AddAsync(newBranch);
                await _branchRepository.SaveChangeAsync();

                return new SuccessDataResult<BranchDTO>(newBranch.Adapt<BranchDTO>(), Messages.BRANCH_ADD_SUCCESS);
            }
            catch (Exception)
            {
                return new ErrorDataResult<BranchDTO>(Messages.BRANCH_ADD_ERROR);
            }          
        }

        //Belirli bir company kimliğine ait branchleri döndürür. 
        public async Task<List<Branch>> GetBranchesByCompanyIdAsync(Guid companyId)
        {
            var allBranches = await _branchRepository.GetAllAsync();
            return allBranches.Where(branch => branch.CompanyId == companyId).ToList();
        }

        //Belirtilen bir şubeyi siler ve işlem başarılıysa başarılı bir mesaj döndürür.Herhangi bir hata oluşursa uygun bir hata mesajıyla birlikte hata durumunu döndürür.

        public async Task<IResult> DeleteAsync(Guid branchId)
        {        
            try
            {
                var deletingBranch = await _branchRepository.GetByIdAsync(branchId);

                await _branchRepository.DeleteAsync(deletingBranch);
                await _branchRepository.SaveChangeAsync();

                return new SuccessResult(Messages.BRANCH_DELETE_SUCCESS);
            }
            catch (Exception)
            {
                return new ErrorResult(Messages.BRANCH_DELETE_ERROR);
            }            
        }

        //Tüm şubeleri getirir ve işlem başarılıysa şube listesini döndürür.Eğer hiç şube bulunamazsa uygun bir mesajla birlikte hata durumunu döndürür.
        public async Task<IDataResult<List<BranchListDTO>>> GetAllAsync()
        {            
            try
            {
                IEnumerable<Branch> branches;
                branches = await _branchRepository.GetAllAsync();

                if (branches.Count() == 0)
                {
                    return new ErrorDataResult<List<BranchListDTO>>(new List<BranchListDTO>(), Messages.BRANCH_LISTED_NOTFOUND);
                }

                var companies = await _companyRepository.GetAllAsync();
                var branchListDTOs = branches.Adapt<List<BranchListDTO>>();

                foreach (var branchDTO in branchListDTOs)
                {
                    var company = companies.FirstOrDefault(b => b.Id == branchDTO.CompanyId);

                    if (company != null) branchDTO.CompanyName = company.Name;

                    else branchDTO.CompanyName =Messages.BRANCH_COMPANY_LISTED_DELETED;
                }                

                return new SuccessDataResult<List<BranchListDTO>>(branchListDTOs,Messages.BRANCH_LISTED_SUCCESS);
            }

            catch (Exception)
            {
                return new ErrorDataResult<List<BranchListDTO>>(new List<BranchListDTO>(), Messages.BRANCH_LISTED_ERROR);
            }
        }

        //Belirli bir şube kimliğine göre şubeyi getirir.Şube bulunamazsa uygun bir hata mesajıyla birlikte hata durumunu döndürür.
        public async Task<IDataResult<BranchDTO>> GetByIdAsync(Guid branchId)
        {
            try
            {
                var branch = await _branchRepository.GetByIdAsync(branchId);
                if (branch == null)
                {
                    return new ErrorDataResult<BranchDTO>(Messages.BRANCH_GETBYID_ERROR);
                }
                var branchDTO = branch.Adapt<BranchDTO>();
                var company = await _companyRepository.GetByIdAsync(branch.CompanyId);

                if (company != null)
                {
                    branchDTO.CompanyName = company.Name;
                }

                return new SuccessDataResult<BranchDTO>(branch.Adapt<BranchDTO>(), Messages.BRANCH_GETBYID_SUCCESS);
            }
            catch
            {
                return new ErrorDataResult<BranchDTO>(Messages.BRANCH_GETBYID_ERROR);
            }               
        }

        //Belirli bir şube kimliğine göre şube bilgilerini günceller.Güncelleme başarılıysa güncellenen şube bilgilerini döndürür.Herhangi bir hata oluşursa uygun bir hata mesajıyla birlikte hata durumunu döndürür.
        public async Task<IDataResult<BranchDTO>> UpdateAsync(BranchUpdateDTO branchUpdateDTO)
        {
            try
            {            
                var updatingBranch = await _branchRepository.GetByIdAsync(branchUpdateDTO.Id);

                if (updatingBranch == null)
                {
                    return new ErrorDataResult<BranchDTO>(Messages.BRANCH_GETBYID_ERROR);
                }

                var company = await _companyRepository.GetByIdAsync(branchUpdateDTO.CompanyId);

                if (company == null)
                {
                    return new ErrorDataResult<BranchDTO>(Messages.COMPANY_GETBYID_ERROR);
                }

                await _branchRepository.UpdateAsync(branchUpdateDTO.Adapt(updatingBranch));
                await _branchRepository.SaveChangeAsync();
                
                return new SuccessDataResult<BranchDTO>(updatingBranch.Adapt<BranchDTO>(), Messages.BRANCH_UPDATE_SUCCESS);
            }
            catch (Exception)
            {
                return new ErrorDataResult<BranchDTO>(Messages.BRANCH_UPDATE_ERROR);
            }           
        }
    }
}