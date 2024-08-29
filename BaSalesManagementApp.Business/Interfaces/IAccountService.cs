using BaSalesManagementApp.Business.Results;
using BaSalesManagementApp.Dtos.MyProfileDTO;

namespace BaSalesManagementApp.Business.Interfaces
{
    public interface IAccountService
    {
        Task<bool> AnyAsync(Expression<Func<IdentityUser, bool>> expression);
		Task<CreateUserResult> CreateUserAsync(IdentityUser identityUser, Roles role);
		Task<IdentityResult> DeleteUserAsync(string identityId);
        Task<IdentityUser?> FindByIdAsync(string identityId);
        /// <summary>
        /// Email adresine göre identity user nesnesi getirir.
        /// </summary>
        /// <param name="email">Aranan kullanıcıya ait email adresi.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev.Görev sonucunda identity verileri döner, bulunamazsa null döner.</returns>
        Task<IdentityUser> FindByEmailAsync(string email);
        /// <summary>
        /// AppUser nesnesinin rollerini getirir.
        /// </summary>
        /// <param name="appUser">Rolleri getirilecek olan identityuser nesnesi.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev.Görev sonucunda string olarak appUser nesnesinin rolleri döner, bulunamazsa null döner.</returns>
        Task<IEnumerable<string>> GetRolesAsync(IdentityUser appUser);
        /// <summary>
        /// İstenilen parametrelere bağlı olarak kullanıcının id verisini getirir.
        /// </summary>
        /// <param name="identityId">Sistemde kayıtlı kullanıcıya ait identityId.</param>
        /// <param name="role">Sistemde kayıtlı kullanıcının rolü.</param>
        /// <returns>Asenkron işlemi temsil eder.Görev sonucunda guid tipinde kullanıcnın id verisini döner.</returns>
        Task<Guid> GetUserIdAsync(string identityId, string role);
        /// <summary>
        /// Sisteme giriş işleminin sonucunu getirir.
        /// </summary>
        /// <param name="appUser">Sisteme giriş yapacak olan kullanıcıya ait appUser nesnesi.</param>
        /// <param name="password">Sisteme giriş yapacak kullanıcıya ait şifre.</param>
        /// <param name="isPersistant">Kimlik doğrulama oturumunun birden çok istekte kalıcı olup olmadığını alır veya ayarlar.</param>
        /// <param name="isLockoutOnFailure">Oturum açma başarısız olursa kullanıcı hesabının kilitlenmesi gerekip gerekmediğini belirten bayrak.</param>
        /// <returns>Asenkron işlemi temsil eden görev.Görev sonucunda SignInResult tipinde sonucu döner.</returns>
        Task<SignInResult> SignInAsync(IdentityUser appUser, string password, bool isPersistent = false, bool isLockoutOnFailure = false);
        /// <summary>
        /// Sistemden çıkış işlemini gerçekleştirir.
        /// </summary>
        /// <returns>Asenkreon işlemi temsil eden görev.</returns>
        Task SignOutAsync();
        /// <summary>
        /// Sistemde olan kullanıcının MyProfile sayfasına gitmesi ve bilgilerini görmesi
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Asenkron işlemi temsil eder.</returns>
        Task<MyProfileDTO> GetProfileAsync(string userId);
        /// <summary>
        /// Sistemde olan kullanıcının MyProfile sayfasındaki bilgilerini güncelleme işlemini gerçekleştirir.
        /// </summary>
        /// <param name="profileDTO"></param>
        /// <returns>Asenkron işlemi temsil eder</returns>
        Task<MyProfileDTO> UpdateProfileAsync(MyProfileDTO profileDTO);
    }
}
