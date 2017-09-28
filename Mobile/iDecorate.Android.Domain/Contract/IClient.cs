using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;


namespace iDecorate.Android.Domain.Contract
{
    public interface IClient<T>
    {
        Task<T> Get(string id);
        Task<IEnumerable<T>> GetList();
        Task<IEnumerable<T>> Post(T body);
        Task<IEnumerable<T>> Put(T body);
        Task<IEnumerable<T>> Delete(string id);
    }
}