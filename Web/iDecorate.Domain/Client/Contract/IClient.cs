using System.Collections.Generic;
using System.Threading.Tasks;

namespace iDecorate.Domain.Client.Contract
{
    public interface IClient<T>
    {
        Task<T> Get(string id);
        Task<IEnumerable<T>> GetList();
        Task<bool> Post(T body);
        Task<bool> Put(T body);
        Task<bool> Delete(string id);
    }
}