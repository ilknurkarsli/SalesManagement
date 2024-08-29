using BaSalesManagementApp.Dtos.UserInfoDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaSalesManagementApp.Business.Interfaces
{
    public interface IUserCardService
    {
        // UserCard Bilgilerinin Getirilmesi İçin Oluşturulan Service ve Metodu
        Task<UserInfoDTO> GetUserAsync(string userId);
    }
}
