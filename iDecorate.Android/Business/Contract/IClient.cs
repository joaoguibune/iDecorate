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

namespace iDecorate.Android.Business.Contract
{
    public interface IClient<T>
    {
        Task<T> Get();
        Task<IEnumerable<T>> GetList();
        void Post(T body);
        void Put(T body);
        void Get(string id);
    }
}