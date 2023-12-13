using Microsoft.EntityFrameworkCore;
using SpeechBackend.DAL.Repos.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechBackend.DAL.Repos.Realization
{
    public abstract class GenericRepository<T> : IRepository<T> where T : class
    {
        protected readonly DatabaseContext context;
        protected readonly DbSet<T> table;

        protected GenericRepository(DatabaseContext context)
        {
            this.context = context;
            table = this.context.Set<T>();
        }

        public virtual async Task Delete(int id)
        {
            var item = await table.FindAsync(id);
            table.Remove(item!);
            await context.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            var items = await table.ToListAsync();
            return items;
        }

        public abstract Task<T> GetCompleteById(int id);

        public virtual async Task<T> GetById(int id)
        {
            var item = await table.FindAsync(id);
            return item!;
        }

        public virtual async Task Insert(T entity)
        {
            await table.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public virtual async Task Update(T entity)
        {
            table.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
