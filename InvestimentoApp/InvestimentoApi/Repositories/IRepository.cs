using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestimentoApi.Repositories
{
    public interface IRepository<T>
    {
        public abstract Task Add(T item);
        public abstract Task Remove(int id);
        public abstract Task Update(T item);
        public abstract Task<T> FindByID(int id);
        public abstract Task<IEnumerable<T>> FindAll();
    }
}
