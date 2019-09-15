using System.Threading.Tasks;
using EcoEye.DTO;
using EcoEye.DTO.Google.Response;

namespace EcoEye.Service.Classification
{
    public interface IClassificationService
    {
        ImageClassificationResponseDTO ConvertToClassificationDto(GoogleVisionResponse googleVisionResponse);
    }
}