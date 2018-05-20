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
using System.Net.Http.Headers;

namespace PetApplication.Service
{
    public class GetPetServiceData : IGetPetServiceData
    {
        string petOwnerResult;
        MemoryCache cache = MemoryCache.Default;

        public IEnumerable<PetOwnerModel> GetPetDataFromService()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
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
                    var settings = new Newtonsoft.Json.JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };

                    return JsonConvert.DeserializeObject<List<PetOwnerModel>>(petOwnerResult, settings);
                }
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                throw;
            }
        }
    }
}
