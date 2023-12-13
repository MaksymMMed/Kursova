using SpeechBackend.BLL.Services.Interfaces;
using SpeechBackend.DAL.Entity;
using SpeechBackend.DAL.Repos.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechBackend.BLL.Services.Realization
{
    public class SentenceService : ISentenceService
    {
        readonly ISentenceRepository repository;

        public SentenceService(ISentenceRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Sentence> GetSentence(string id)
        {
            var item = await repository.GetById(int.Parse(id.Trim()));
            return item;
        }
    }
}
