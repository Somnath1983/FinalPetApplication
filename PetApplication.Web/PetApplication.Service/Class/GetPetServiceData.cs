using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetApplication.Entity;
using System.Runtime.Caching;
using System.Net.Http;
using System.Configuration;
using PetApplication.Utility;
using Newtonsoft.Json;

namespace PetApplication.Service
{
    public class GetPetServiceData : IGetPetServiceData
    {
        string petOwnerResult;
        MemoryCache cache = MemoryCache.Default;

        public IEnumerable<PetOwnerModel> GetPetDataFromService()
        {
            using (var httpClient = new HttpClient())
            {
                //If MemoryCaching Needed
                bool isMemoryCacheEnabled = Convert.ToBoolean(ConfigurationManager.AppSettings[Constant.IsMemoryCacheEnabled]);
                //Check in Cache
                if (cache.Get(Constant.CacheKey) == null)
                {
                    petOwnerResult = httpClient.GetStringAsync(new Uri(ConfigurationManager.AppSettings[Constant.ApiUrl])).Result;
                    if (isMemoryCacheEnabled)
                        cache.Add(Constant.CacheKey, petOwnerResult, new CacheItemPolicy { AbsoluteExpiration = DateTime.Now.AddSeconds(60) });
                }
                else
                    petOwnerResult = Convert.ToString(cache.Get(Constant.CacheKey));

                return JsonConvert.DeserializeObject<List<PetOwnerModel>>(petOwnerResult);
            }
        }
    }
}
