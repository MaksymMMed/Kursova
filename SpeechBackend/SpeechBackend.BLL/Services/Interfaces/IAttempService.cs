using SpeechBackend.BLL.DTO.Request;
using SpeechBackend.BLL.DTO.Response;
using SpeechBackend.DAL.Entity;

namespace SpeechBackend.BLL.Services.Interfaces
{
    public interface IAttempService
    {
        Task AddAttemp(AddAttempRequest request);
        Task<GetAttempResponse> GetAttemp(int id);
        public Task<List<Attemp>> GetUserAttemps(int id);
    }
}