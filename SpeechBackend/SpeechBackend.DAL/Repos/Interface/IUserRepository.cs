using SpeechBackend.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechBackend.DAL.Repos.Interface
{
    public interface IUserRepository:IRepository<User>
    {
        public Task<User> GetUserByEmail(string email,string password);
    }
}
