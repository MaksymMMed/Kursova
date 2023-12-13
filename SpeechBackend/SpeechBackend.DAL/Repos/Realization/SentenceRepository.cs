using Microsoft.EntityFrameworkCore;
using SpeechBackend.DAL.Entity;
using SpeechBackend.DAL.Repos.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechBackend.DAL.Repos.Realization
{
    public class SentenceRepository : GenericRepository<Sentence>, ISentenceRepository
    {
        public SentenceRepository(DatabaseContext context) : base(context)
        {
        }

        public async override Task<Sentence> GetCompleteById(int id)
        {
            Sentence? sentence = await table.Where(x => x.Id == id).Include(x => x.Attemps).FirstOrDefaultAsync();
            if (sentence == null)
            {
                throw new Exception("Sentence not found");
            }
            return sentence;
        }
        public async Task<Sentence> GetRandomSentence()
        {
            Sentence? sentence = await table.OrderBy(x=>Guid.NewGuid()).FirstOrDefaultAsync();
            return sentence!;
        }

        
    }
}
