using BaSalesManagementApp.Business.Interfaces;
using BaSalesManagementApp.Business.Results;
using BaSalesManagementApp.Dtos.MyProfileDTO;

namespace BaSalesManagementApp.Business.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IAdminRepository adminRepository;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IPasswordGeneratorService passwordGeneratorService;

        public AccountService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IAdminRepository adminRepository, IEmployeeRepository employeeRepository, IPasswordGeneratorService passwordGeneratorService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.adminRepository = adminRepository;
            this.employeeRepository = employeeRepository;
            this.passwordGeneratorService = passwordGeneratorService;
        }

        public async Task<bool> AnyAsync(Expression<Func<IdentityUser, bool>> expression)
        {
            return await userManager.Users.AnyAsync(expression);
        }

        public async Task<CreateUserResult> CreateUserAsync(IdentityUser identityUser, Roles role)
        {
            string defaultPassword = await passwordGeneratorService.GenerateRandomPasswordAsync();
            var result = await userManager.CreateAsync(identityUser, defaultPassword);
            if (!result.Succeeded)
            {
                return new CreateUserResult { IdentityResult = result, Password = null };
            }
            var roleResult = await userManager.AddToRoleAsync(identityUser, role.ToString());
            return new CreateUserResult { IdentityResult = roleResult, Password = defaultPassword };
        }


        public async Task<IdentityResult> DeleteUserAsync(string identityId)
        {
            var user = await userManager.FindByIdAsync(identityId);
            if (user is null)
            {
                return IdentityResult.Failed(new IdentityError()
                {
                    Code = "Kullanıcı Bulunamadı",
                    Description = "Kullanıcı Bulunamadı"
                });
            }
            return await userManager.DeleteAsync(user);
        }

        public async Task<IdentityUser?> FindByIdAsync(string identityId)
        {
            return await userManager.FindByIdAsync(identityId);
        }

        public async Task<IdentityUser> FindByEmailAsync(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        public async Task<IEnumerable<string>> GetRolesAsync(IdentityUser appUser)
        {
            return await userManager.GetRolesAsync(appUser);
        }

        public async Task<Guid> GetUserIdAsync(string identityId, string role)
        {
            BaseUser? user = role switch
            {
                "Admin" => await adminRepository.GetByIdentityId(identityId),
                "Employee" => await employeeRepository.GetByIdentityId(identityId),
                _ => null
            };
            return user is null ? Guid.NewGuid() : user.Id;
        }

        public async Task<SignInResult> SignInAsync(IdentityUser appUser, string password, bool isPersistent = false, bool isLockoutOnFailure = false)
        {
            return await signInManager.PasswordSignInAsync(appUser, password, isPersistent, isLockoutOnFailure);
        }

        public async Task SignOutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<MyProfileDTO> GetProfileAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return null;

            BaseUser baseUser = await adminRepository.GetByIdentityId(userId)
                                ?? await employeeRepository.GetByIdentityId(userId) as BaseUser;

            if (baseUser == null)
                return null;

            // Mapster kullanarak BaseUser veya türevlerini MyProfileDTO'ya dönüştürme
            MyProfileDTO profileDTO = baseUser.Adapt<MyProfileDTO>();
            profileDTO.Id = Guid.Parse(userId); // Id özelliğini doğru formatta atama

            return profileDTO;
        }

        public async Task<MyProfileDTO> UpdateProfileAsync(MyProfileDTO profileDTO)
        {
            var userId = profileDTO.Id.ToString();
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
                return null;

            BaseUser baseUser = await adminRepository.GetByIdentityId(userId)
                                ?? await employeeRepository.GetByIdentityId(userId) as BaseUser;

            if (baseUser == null)
                return null;

            // Mevcut Id özelliğini koruyarak öznitelik atamalarını yapın
            var originalId = baseUser.Id;
            if (baseUser is Admin adminUser)
            {
                profileDTO.Adapt(adminUser);
                adminUser.Id = originalId; // Id özelliğini koru
                await adminRepository.UpdateAsync(adminUser);
            }
            else if (baseUser is Employee employeeUser)
            {
                profileDTO.Adapt(employeeUser);
                employeeUser.Id = originalId; // Id özelliğini koru
                await employeeRepository.UpdateAsync(employeeUser);
            }

            var updateResult = await userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
                return null;


            if (baseUser is Admin)
            {
                await adminRepository.SaveChangeAsync();
            }
            else if (baseUser is Employee)
            {
                await employeeRepository.SaveChangeAsync();
            }

            return profileDTO;
        }
    }
}
