using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using FirebaseProject_Example.Models;

namespace FirebaseProject_Example.GenericService
{
    public class FirebaseHelper<T>:IFirebaseHelper<T>
    {
        readonly string BASE_URL = "https://*********.firebaseio.com/";
        readonly string AUTH = "VBu1CdQfFiOMz17UyeF65m7o54EAYxJu2rS43fi8"; // AUTH KEY (FİREBASE) !
        readonly string ROOT_KEY = typeof(T).Name;

        private FirebaseClient GetFirebaseClient()
        {
            return new FirebaseClient(BASE_URL, new FirebaseOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(AUTH)
            });
        }

        public async Task Add(string childKey, T data)
        {
            try
            {
                FirebaseClient client = GetFirebaseClient();
                await client.Child(ROOT_KEY)
                            .Child(childKey)
                            .PutAsync(JsonConvert.SerializeObject(data));
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            try
            {
                FirebaseClient client = GetFirebaseClient();
                var datas = await client.Child(ROOT_KEY)
                                        .OnceAsync<T>();

                return datas.Select(u => u.Object);
            }
            catch (System.Exception)
            {

                throw;
            }
            
        }

        public async Task<T> Get(string Id)
        {
            try
            {

                FirebaseClient client = GetFirebaseClient();
                var datas = await client.Child(ROOT_KEY)
                                        .OnceAsync<T>();
                return datas.Where(u => u.Key == Id).FirstOrDefault().Object;

            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task<bool> Delete(string key)
        {
            FirebaseClient client = GetFirebaseClient();
            try
            {
                await client.Child(ROOT_KEY)
                    .Child(key)
                    .DeleteAsync();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<bool> Update(City city)
        {
            FirebaseClient client = GetFirebaseClient();
            try
            {
                await client.Child(ROOT_KEY)
                      .Child(city.Id.ToString())
                      .PutAsync(city);

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}
