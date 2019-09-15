using System.Collections.Generic;

namespace EcoEye.DTO.Google
{
    public class GoogleVisionRequest
    {
        public List<ImageRecognitionRequestDTO> Requests { get; set; } = new List<ImageRecognitionRequestDTO>();
    }
}