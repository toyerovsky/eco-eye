using System.Threading.Tasks;
using EcoEye.DTO.Google;
using EcoEye.DTO.Google.Response;

namespace EcoEye.Service.Google
{
    public interface IGoogleService
    {
        Task<GoogleVisionResponse> GetGoogleVisionResponseAsync(GoogleVisionRequest dto);
    }
}