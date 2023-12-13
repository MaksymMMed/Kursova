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
    public class AttempRepository : GenericRepository<Attemp>, IAttempRepository
    {
        public AttempRepository(DatabaseContext context) : base(context)
        {
        }

        public override async Task<Attemp> GetCompleteById(int id)
        {
            Attemp? attemp = await table.Where(x=>x.Id == id).Include(x=>x.Sentence).Include(x=>x.User).FirstOrDefaultAsync();
            if (attemp == null)
            {
                throw new Exception("Attemp not found");
            }
            return attemp;
            
        }

        public async Task<List<Attemp>> GetUserSentences(int id)
        {
            List<Attemp> list = await table.Where(x => x.UserId == id).ToListAsync();
            return list!;
        }
    }
}
