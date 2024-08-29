using BaSalesManagementApp.DataAccess.Context;
using BaSalesManagementApp.Dtos.UserInfoDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaSalesManagementApp.Business.Services
{
    public class UserCardService :IUserCardService
    {
        private readonly BaSalesManagementAppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UserCardService(BaSalesManagementAppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<UserInfoDTO> GetUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return null;
            }

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();

            // Admin veya Employee olup olmadığını kontrol et
            var admin = await _context.Admins.FirstOrDefaultAsync(x=>x.IdentityId == user.Id);
            if (admin != null)
            {
                return new UserInfoDTO
                {
                    FirstName = admin.FirstName,
                    LastName = admin.LastName,
                    PhotoData = admin.PhotoData,
                    Role = role
                };
            }

            var employee = await _context.Employees.FirstOrDefaultAsync(x => x.IdentityId == user.Id);
            if (employee != null)
            {
                if (employee.Title != null ) 
                {
                    if (employee.Title == "Manager")
                    {
						return new UserInfoDTO
						{
							FirstName = employee.FirstName,
							LastName = employee.LastName,
							PhotoData = employee.PhotoData,
							Role = employee.Title
						};
					}
                    else if (employee.Title == "Employee")
                    {

                        return new UserInfoDTO
                        {
                            FirstName = employee.FirstName,
                            LastName = employee.LastName,
                            PhotoData = employee.PhotoData,
                            Role = employee.Title
                        };
                    }
				}
            }

            return null;
        }
    }
}
