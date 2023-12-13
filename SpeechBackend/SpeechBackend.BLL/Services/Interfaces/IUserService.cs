using SpeechBackend.BLL.DTO.Request;
using SpeechBackend.BLL.DTO.Response;

namespace SpeechBackend.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task AddUser(AddUserRequest request);
        Task DeleteUser(int id);
        Task<GetUserResponse> GetUserById(int id);
        Task<GetUserResponse> GetUserByEmail(string email,string password);
        Task UpdateUser(AddUserRequest request);
    }
}