using System.Threading.Tasks;
using DataAccessLayer.Models;
using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.Interfaces;

public interface IUserService
{
    Task<UserInfoDTO> GetUserInfoByIdAsync(int id);

    Task<List<UserInfoDTO>> GetUsersInfoAsync();

    Task<LoginRes?> AuthenticateAsync(LoginReq model);

    Task<LoginRes?> RegisterAsync(UserDTO userModel);
}
