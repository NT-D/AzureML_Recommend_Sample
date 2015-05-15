using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MLRecommendAPI.Controllers
{

    //今回使用するデータ型。分かりやすさのため、API と同じファイルに記載しています。
    public class StringTable
    {
        public string[] ColumnNames { get; set; }
        public string[,] Values { get; set; }
    }

    public class ValuesController : ApiController
    {
        //Azure ML API の URL を入力してください。
        private const string apiUrl = "url";

        // Azure ML API の API キーを入力してください。
        private const string apiKey = "apikey"; 
        
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5 等で呼び出し可能。ML側のAPIを呼び出し、ID=5 のユーザーに推薦する映画情報が取得できます。
        public async Task<String> Get(int id)
        {
            var result = "";
            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {

                    Inputs = new Dictionary<string, StringTable>() {
                        {
                            "input1",
                            new StringTable()
                            {
                                ColumnNames = new string[] {"UserId"},
                                Values = new string[,] {  { id.ToString() },  { "0" },  }
                            }
                        },
                                        },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                client.BaseAddress = new Uri(apiUrl);

                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest);

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    result = await response.Content.ReadAsStringAsync();
                }
            }
            return result;
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
