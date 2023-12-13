using SpeechBackend.DAL.Entity;

namespace SpeechBackend.BLL.Services.Interfaces
{
    public interface ISentenceService
    {
        Task<Sentence> GetSentence(string id);
    }
}