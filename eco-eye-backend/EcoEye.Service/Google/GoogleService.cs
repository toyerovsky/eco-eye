using System.Net.Http;
using System.Threading.Tasks;
using EcoEye.DTO.Google;
using EcoEye.DTO.Google.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EcoEye.Service.Google
{
    public class GoogleService : IGoogleService
    {
        public async Task<GoogleVisionResponse> GetGoogleVisionResponseAsync(GoogleVisionRequest dto)
        {
            using (var client = new HttpClient())
            {
                
                const string key = "AIzaSyBf6l9N5cvfsr4NsOU-SSkXOlHiyYVs6_o";
                var url =
                    $"https://vision.googleapis.com/v1/images:annotate?key={key}";
                var serializerSettings = new JsonSerializerSettings();
                serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                var httpContent = new StringContent(JsonConvert.SerializeObject(dto,serializerSettings));

                var response = await client.PostAsync(url, httpContent);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<GoogleVisionResponse>(jsonResponse);
            }
        }
    }
}