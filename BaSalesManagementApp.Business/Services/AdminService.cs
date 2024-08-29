using BaSalesManagementApp.Dtos.AdminDTOs;
using BaSalesManagementApp.Dtos.EmployeeDTOs;
using BaSalesManagementApp.Dtos.MailDTOs;
using BaSalesManagementApp.Entites.DbSets;

namespace BaSalesManagementApp.Business.Services
{
	public class AdminService : IAdminService
	{
		private readonly IAdminRepository _adminRepository;
		private readonly IAccountService _accountService;
		private readonly IMailService _mailService;

		public AdminService(IAdminRepository adminRepository, IAccountService accountService, IMailService mailService)
		{
			_adminRepository = adminRepository;
			_accountService = accountService;
			_mailService = mailService;
		}

		public async Task<IDataResult<AdminDTO>> AddAsync(AdminCreateDTO adminCreateDTO)
		{
			if (await _accountService.AnyAsync(x => x.Email == adminCreateDTO.Email))
			{
				return new ErrorDataResult<AdminDTO>(Messages.ADMIN_EMAIL_IN_USE);
			}
			IdentityUser identityUser = new()
			{
				Email = adminCreateDTO.Email,
				EmailConfirmed = false,
				UserName = adminCreateDTO.Email
			};

			DataResult<AdminDTO> result = new ErrorDataResult<AdminDTO>(Messages.ADMIN_CREATE_ERROR);
			var strategy = await _adminRepository.CreateExecutionStrategy();

			await strategy.ExecuteAsync(async () =>
			{
				using var transactionScope = await _adminRepository.BeginTransactionAsync().ConfigureAwait(false);
				try
				{
					var createUserResult = await _accountService.CreateUserAsync(identityUser, Roles.Admin);
					var identityResult = createUserResult.IdentityResult;
					var defaultPassword = createUserResult.Password;

					if (!identityResult.Succeeded)
					{
						result = new ErrorDataResult<AdminDTO>(Messages.ADMIN_CREATE_ERROR);
						transactionScope.Rollback();
						return;
					}

					var admin = adminCreateDTO.Adapt<Admin>();
					admin.IdentityId = identityUser.Id;
					await _adminRepository.AddAsync(admin);
					await _adminRepository.SaveChangeAsync();

					// E-Posta Gönderim işlemi
					var mailCreateDto = new MailCreateDto
					{
						Title = "Merhaba,",
						Subject = "Şirket Hesabın Hakkında Bilgilendirme",
						Body = $"Merhaba, {admin.FirstName}, <br><br>Kullanıcı Adınız : {identityUser.UserName} <br>Şifreniz : {defaultPassword}<br><br>Lütfen sisteme giriş yaparak şifrenizi güncelleyiniz.",
						ReceiverMailAddress = admin.Email,
					};
					await _mailService.SendMailAsync(mailCreateDto);
					result = new SuccessDataResult<AdminDTO>(admin.Adapt<AdminDTO>(), Messages.ADMIN_CREATED_SUCCESS);
					transactionScope.Commit();
				}
				catch (Exception ex)
				{
					result = new ErrorDataResult<AdminDTO>(Messages.ADMIN_CREATE_ERROR + ex.Message);
					transactionScope.Rollback();
				}
				finally
				{
					transactionScope.Dispose();
				}
			});
			return result;
		}

		public async Task<IResult> DeleteAsync(Guid adminId)
		{
			var admin = await _adminRepository.GetByIdAsync(adminId);
			if (admin == null)
			{
				return new ErrorResult(Messages.ADMIN_DELETE_ERROR);
			}
			DataResult<AdminDTO> result = new ErrorDataResult<AdminDTO>();
			var strategy = await _adminRepository.CreateExecutionStrategy();
			await strategy.ExecuteAsync(async () =>
			{
				using var transactionScope = await _adminRepository.BeginTransactionAsync().ConfigureAwait(false);

				try
				{
					var identityResult = await _accountService.DeleteUserAsync(admin.IdentityId);
					if (!identityResult.Succeeded)
					{
						result = new ErrorDataResult<AdminDTO>(Messages.ADMIN_DELETE_ERROR + identityResult.Errors.FirstOrDefault()?.Description);
						return;
					}

					await _adminRepository.DeleteAsync(admin);
					await _adminRepository.SaveChangeAsync();

					await transactionScope.CommitAsync();

					result = new SuccessDataResult<AdminDTO>(Messages.ADMIN_DELETED_SUCCESS);
				}
				catch (Exception ex)
				{
					result = new ErrorDataResult<AdminDTO>(Messages.ADMIN_DELETE_ERROR + ex.Message);
					await transactionScope.RollbackAsync();
				}
				finally
				{
					await transactionScope.DisposeAsync();
				}
			});
			return result;
		}

		public async Task<IDataResult<List<AdminListDTO>>> GetAllAsync()
		{
			var admins = await _adminRepository.GetAllAsync();
			var adminList = admins.Adapt<List<AdminListDTO>>();
			if (adminList == null || adminList.Count == 0)
			{
				return new ErrorDataResult<List<AdminListDTO>>(adminList, Messages.ADMIN_LISTED_NOTFOUND);
			}

			return new SuccessDataResult<List<AdminListDTO>>(admins.Adapt<List<AdminListDTO>>(), Messages.ADMIN_LISTED_SUCCESS);
		}

		public async Task<IDataResult<List<AdminListDTO>>> GetAllAsync(string sortAdmin)
		{
			var admins = await _adminRepository.GetAllAsync();

			// Sıralama işlemi (Email sıralama seçenekleri kaldırıldı)
			admins = sortAdmin.ToLower() switch
			{
				"name" => admins.OrderBy(a => a.FirstName).ToList(),
				"namedesc" => admins.OrderByDescending(a => a.FirstName).ToList(),
				"createddate" => admins.OrderBy(a => a.CreatedDate).ToList(),
				"createddatedesc" => admins.OrderByDescending(a => a.CreatedDate).ToList(),
				_ => admins.OrderBy(a => a.FirstName).ToList(), // Varsayılan olarak isim sıralaması (A-Z)
			};

			var adminList = admins.Adapt<List<AdminListDTO>>();
			if (adminList == null || adminList.Count == 0)
			{
				return new ErrorDataResult<List<AdminListDTO>>(adminList, Messages.ADMIN_LISTED_NOTFOUND);
			}

			return new SuccessDataResult<List<AdminListDTO>>(adminList, Messages.ADMIN_LISTED_SUCCESS);
		}

		public async Task<IDataResult<AdminDTO>> GetByIdAsync(Guid adminId)
		{
			var admin = await _adminRepository.GetByIdAsync(adminId);
			if (admin == null)
			{
				return new ErrorDataResult<AdminDTO>(Messages.ADMIN_GETBYID_ERROR);
			}

			return new SuccessDataResult<AdminDTO>(admin.Adapt<AdminDTO>(), Messages.ADMIN_GETBYID_SUCCESS);
		}

		public async Task<IDataResult<AdminDTO>> UpdateAsync(AdminUpdateDTO adminUpdateDTO)
		{
			var existingAdmin = await _adminRepository.GetByIdAsync(adminUpdateDTO.Id);
			if (existingAdmin == null)
			{
				return new ErrorDataResult<AdminDTO>(Messages.ADMIN_GETBYID_ERROR);
			}

			DataResult<AdminDTO> result = new ErrorDataResult<AdminDTO>();
			var strategy = await _adminRepository.CreateExecutionStrategy();
			await strategy.ExecuteAsync(async () =>
			{
				using var transactionScope = await _adminRepository.BeginTransactionAsync().ConfigureAwait(false);

				try
				{
					existingAdmin.FirstName = adminUpdateDTO.FirstName;
					existingAdmin.LastName = adminUpdateDTO.LastName;
					existingAdmin.Email = adminUpdateDTO.Email;
					if (adminUpdateDTO.PhotoData != null && adminUpdateDTO.PhotoData.Length > 0)
					{
						existingAdmin.PhotoData = adminUpdateDTO.PhotoData;
					}

					await _adminRepository.UpdateAsync(existingAdmin);
					await _adminRepository.SaveChangeAsync();

					await transactionScope.CommitAsync();

					result = new SuccessDataResult<AdminDTO>(existingAdmin.Adapt<AdminDTO>(), Messages.ADMIN_UPDATE_SUCCESS);
				}
				catch (Exception ex)
				{
					result = new ErrorDataResult<AdminDTO>(Messages.ADMIN_UPDATE_ERROR + ex.Message);
					await transactionScope.RollbackAsync();
				}
				finally
				{
					await transactionScope.DisposeAsync();
				}

				return new SuccessDataResult<AdminDTO>(existingAdmin.Adapt<AdminDTO>(), Messages.ADMIN_UPDATE_SUCCESS);
			});

			return result;
		}
	}
}
