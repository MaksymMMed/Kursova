using SpeechBackend.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechBackend.DAL.Repos.Interface
{
    public interface ISentenceRepository:IRepository<Sentence>
    {
        public Task<Sentence> GetRandomSentence();
    }
}
