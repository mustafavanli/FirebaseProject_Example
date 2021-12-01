using FirebaseProject_Example.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirebaseProject_Example.GenericService
{
    public interface IFirebaseHelper<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task Add(string childKey, T data);
        Task<T> Get(string Id);
        Task<bool> Delete(string key);
        Task<bool> Update(City city);
    }
}
