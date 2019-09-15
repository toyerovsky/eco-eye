using EcoEye.DTO;

namespace EcoEye.Service
{
    public interface IRecognitionService
    {
       ImageClassificationResponseDTO ConvertFromGoogleResponse();
    }
}