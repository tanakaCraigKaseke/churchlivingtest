using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManagementApp.Core.DataTransferObjects;

namespace TimeManagementApp.Core.Interfaces
{
    public interface IUserService
    {
        Task<DataResponseDto> LoginUser(LoginDto loginDetails);
        Task<bool> CheckIfUsernameExists(string username);
        Task<DataResponseDto> RegisterUser(RegisterDto registrationDetail);

        Task<LoggedInUserDto> GetUserById(int userId);
    }
}
