using BaSalesManagementApp.Core.Utilities.Results;
using BaSalesManagementApp.Dtos.EmployeeDTOs;
using BaSalesManagementApp.Dtos.MailDTOs;
using Microsoft.AspNetCore.Identity;

namespace BaSalesManagementApp.Business.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAccountService _accountService;
        private readonly IMailService _mailService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public EmployeeService(IEmployeeRepository employeeRepository, IAccountService accountService, IMailService mailService, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _employeeRepository = employeeRepository;
            _accountService = accountService;
            _mailService = mailService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IDataResult<EmployeeDTO>> AddAsync(EmployeeCreateDTO employeeCreateDTO)
        {
            if (await _accountService.AnyAsync(x => x.Email == employeeCreateDTO.Email))
            {
                return new ErrorDataResult<EmployeeDTO>(Messages.EMPLOYEE_EMAIL_IN_USE);
            }

            IdentityUser identityUser = new()
            {
                Email = employeeCreateDTO.Email,
                EmailConfirmed = false,
                UserName = employeeCreateDTO.Email
            };

            DataResult<EmployeeDTO> result = new ErrorDataResult<EmployeeDTO>(Messages.EMPLOYEE_ADD_ERROR);
            var strategy = await _employeeRepository.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                using var transactionScope = await _employeeRepository.BeginTransactionAsync().ConfigureAwait(false);
                try
                {
                    // Rolü adı ile bul
                    var role = await _roleManager.FindByNameAsync(employeeCreateDTO.Title);
                    if (role == null)
                    {
                        result = new ErrorDataResult<EmployeeDTO>(Messages.EMPLOYEE_ADD_ERROR);
                        await transactionScope.RollbackAsync();
                        return;
                    }

                    // Rol enum'ını al (Eğer enum kullanmaya devam edecekseniz)
                    if (!Enum.TryParse(role.Name, out Roles roleEnum))
                    {
                        result = new ErrorDataResult<EmployeeDTO>(Messages.EMPLOYEE_ADD_ERROR);
                        await transactionScope.RollbackAsync();
                        return;
                    }

                    // Kullanıcıyı oluştur ve şifreyi al
                    var createUserResult = await _accountService.CreateUserAsync(identityUser, roleEnum);
                    var identityResult = createUserResult.IdentityResult;
                    var defaultPassword = createUserResult.Password;

                    if (!identityResult.Succeeded)
                    {
                        result = new ErrorDataResult<EmployeeDTO>(Messages.EMPLOYEE_ADD_ERROR);
                        await transactionScope.RollbackAsync();
                        return;
                    }

                    // Employee verisini oluştur ve kaydet
                    var employee = employeeCreateDTO.Adapt<Employee>();
                    employee.IdentityId = identityUser.Id;
                    employee.Title = role.Name; // Rol ismini Title alanına atayın
                    await _employeeRepository.AddAsync(employee);
                    await _employeeRepository.SaveChangeAsync();

                    // Başarı mesajını belirle
                    if (roleEnum == Roles.Manager)
                    {
                        result = new SuccessDataResult<EmployeeDTO>(employee.Adapt<EmployeeDTO>(), Messages.MANAGER_CREATED_SUCCESS);
                    }
                    else
                    {
                        result = new SuccessDataResult<EmployeeDTO>(employee.Adapt<EmployeeDTO>(),Messages.EMPLOYEE_ADD_SUCCESS);
                    }

                    // E-Posta Gönderim işlemi
                    var mailCreateDto = new MailCreateDto
                    {
                        Title = "Merhaba,",
                        Subject = "Şirket Hesabın Hakkında Bilgilendirme",
                        Body = $"Merhaba, {employee.FirstName}, <br><br>Kullanıcı Adınız : {identityUser.UserName} <br>Şifreniz : {defaultPassword}<br><br>Lütfen sisteme giriş yaparak şifrenizi güncelleyiniz.",
                        ReceiverMailAddress = employee.Email,
                    };
                    await _mailService.SendMailAsync(mailCreateDto);

                    await transactionScope.CommitAsync();
                }
                catch (Exception ex)
                {
                    result = new ErrorDataResult<EmployeeDTO>(Messages.EMPLOYEE_ADD_ERROR + ex.Message);
                    await transactionScope.RollbackAsync();
                }
                finally
                {
                    await transactionScope.DisposeAsync();
                }
            });

            return result;
        }

        public async Task<IDataResult<EmployeeDTO>> GetByIdAsync(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                return new ErrorDataResult<EmployeeDTO>(Messages.EMPLOYEE_GETBYID_ERROR);
            }

            SuccessDataResult<EmployeeDTO> result;

            if (employee.Title == Roles.Manager.ToString())
            {
                result = new SuccessDataResult<EmployeeDTO>(employee.Adapt<EmployeeDTO>(), Messages.MANAGER_FOUND_SUCCESS);
            }
            else
            {
                result = new SuccessDataResult<EmployeeDTO>(employee.Adapt<EmployeeDTO>(), Messages.EMPLOYEE_GETBYID_SUCCESS);
            }

            return result;
        }
        public async Task<IResult> DeleteAsync(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                return new ErrorResult(Messages.EMPLOYEE_DELETE_ERROR);
            }

            DataResult<EmployeeDTO> result = new ErrorDataResult<EmployeeDTO>(Messages.EMPLOYEE_DELETE_ERROR);
            var strategy = await _employeeRepository.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                using var transactionScope = await _employeeRepository.BeginTransactionAsync().ConfigureAwait(false);
                try
                {
                    var identityResult = await _accountService.DeleteUserAsync(employee.IdentityId);
                    if (!identityResult.Succeeded)
                    {
                        result = new ErrorDataResult<EmployeeDTO>(Messages.EMPLOYEE_DELETE_ERROR + identityResult.Errors.FirstOrDefault()?.Description);
                        transactionScope.Rollback();
                        return;
                    }

                    await _employeeRepository.DeleteAsync(employee);
                    await _employeeRepository.SaveChangeAsync();

                    transactionScope.Commit();

                    if (employee.Title == Roles.Employee.ToString())
                    {
                        result = new SuccessDataResult<EmployeeDTO>(employee.Adapt<EmployeeDTO>(),Messages.EMPLOYEE_DELETE_SUCCESS);
                    }
                    else if (employee.Title == Roles.Manager.ToString())
                    {
                        result = new SuccessDataResult<EmployeeDTO>(employee.Adapt<EmployeeDTO>(), Messages.MANAGER_DELETED_SUCCESS);
                    }
                    else if (employee.Title == Roles.Admin.ToString())
                    {
                        result = new SuccessDataResult<EmployeeDTO>(employee.Adapt<EmployeeDTO>(), Messages.ADMIN_DELETED_SUCCESS);
                    }
                }
                catch (Exception ex)
                {
                    result = new ErrorDataResult<EmployeeDTO>(Messages.EMPLOYEE_DELETE_ERROR + ex.Message);
                    await transactionScope.RollbackAsync();
                }
                finally
                {
                    await transactionScope.DisposeAsync();
                }
            });

            return result;
        }

        public async Task<IDataResult<List<EmployeeListDTO>>> GetAllAsync()
        {
            IEnumerable<Employee> employeees;
            try
            {
                employeees = await _employeeRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<EmployeeListDTO>>(new List<EmployeeListDTO>(), Messages.EMPLOYEE_LISTED_ERROR + ex.Message);
            }

            if (employeees.Count() == 0)
            {
                return new ErrorDataResult<List<EmployeeListDTO>>(new List<EmployeeListDTO>(), Messages.EMPLOYEE_LISTED_NOTFOUND);
            }
            return new SuccessDataResult<List<EmployeeListDTO>>(employeees.Adapt<List<EmployeeListDTO>>(), Messages.EMPLOYEE_LISTED_SUCCESS);
        }

        public async Task<IDataResult<EmployeeDTO>> UpdateAsync(EmployeeUpdateDTO employeeUpdateDTO)
        {
            var updatingEmployee = await _employeeRepository.GetByIdAsync(employeeUpdateDTO.Id);
            if (updatingEmployee == null)
            {
                return new ErrorDataResult<EmployeeDTO>(Messages.EMPLOYEE_GETBYID_ERROR);
            }

            var strategy = await _employeeRepository.CreateExecutionStrategy();
            using var transactionScope = await _employeeRepository.BeginTransactionAsync().ConfigureAwait(false);

            try
            {
                updatingEmployee.FirstName = employeeUpdateDTO.FirstName;
                updatingEmployee.LastName = employeeUpdateDTO.LastName;
                updatingEmployee.Email = employeeUpdateDTO.Email;
                updatingEmployee.CompanyId = employeeUpdateDTO.CompanyId;

                if (employeeUpdateDTO.PhotoData != null && employeeUpdateDTO.PhotoData.Length > 0)
                {
                    updatingEmployee.PhotoData = employeeUpdateDTO.PhotoData;
                }

                if (updatingEmployee.Title != employeeUpdateDTO.Title)
                {
                    var user = await _accountService.FindByIdAsync(updatingEmployee.IdentityId);

                    if (user != null)
                    {
                        var currentRoles = await _userManager.GetRolesAsync(user);

                        foreach (var role in currentRoles)
                        {
                            await _userManager.RemoveFromRoleAsync(user, role);
                        }
                        await _userManager.AddToRoleAsync(user, employeeUpdateDTO.Role.ToString());
                        updatingEmployee.Title = employeeUpdateDTO.Title;
                    }
                }

                await _employeeRepository.UpdateAsync(updatingEmployee);
                await _employeeRepository.SaveChangeAsync();

                transactionScope.Commit();
            }
            catch (Exception ex)
            {
                await transactionScope.RollbackAsync();
                return new ErrorDataResult<EmployeeDTO>(Messages.EMPLOYEE_UPDATE_ERROR + ex.Message);
            }
            finally
            {
                await transactionScope.DisposeAsync();
            }

            DataResult<EmployeeDTO> result;

            if (updatingEmployee.Title == Roles.Employee.ToString())
            {
                result = new SuccessDataResult<EmployeeDTO>(updatingEmployee.Adapt<EmployeeDTO>(), Messages.EMPLOYEE_UPDATE_SUCCESS);
            }
            else
            {
                result = new SuccessDataResult<EmployeeDTO>(updatingEmployee.Adapt<EmployeeDTO>(), Messages.MANAGER_UPDATE_SUCCESS);
            }

            return result;
        }
    }
}