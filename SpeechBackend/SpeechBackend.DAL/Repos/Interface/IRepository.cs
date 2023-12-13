using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechBackend.DAL.Repos.Interface
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetById(int id);
        Task Insert(T entity);
        Task Update(T entity);
        Task Delete(int id);
        Task<IEnumerable<T>> GetAll();
    }
}
