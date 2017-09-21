using System.Collections.Generic;

namespace iDecorate.Domain.Server.Contract
{
    public interface IRepository<T>
    {
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        IEnumerable<T> GetAll();
        T Find(int key);
    }
}
