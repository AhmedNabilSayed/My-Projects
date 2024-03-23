using Demo.BLL.Interfaces;
using Demo.DAL.Context;
using Demo.DAL.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly MVCAppDbContext context;

        public GenericRepository(MVCAppDbContext context)
        {
            this.context = context;
        }
        public int Add(T entity)
        {
            context.Set<T>().Add(entity);
            return context.SaveChanges();
        }

        public int Delete(T entity)
        {
            context.Set<T>().Remove(entity);
            return context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        => context.Set<T>().ToList();

        public T GetById(int? id)
        => context.Set<T>().FirstOrDefault(x => x.Id == id);

        public int Update(T entity)
        {
            context.Set<T>().Update(entity);
            return context.SaveChanges();
        }
    }
}
