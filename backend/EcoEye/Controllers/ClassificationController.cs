using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EcoEye.DTO;
using EcoEye.DTO.Google;
using EcoEye.DTO.Google.Response;
using EcoEye.Service.Classification;
using EcoEye.Service.Google;
using Microsoft.AspNetCore.Mvc;

namespace EcoEye.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassificationController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<ImageClassificationResponseDTO>> Classify(ImageClassificationRequestDTO request)
        {
            var service = new GoogleService();
            var classificationService = new ClassificationService();
            ImageRecognitionRequestDTO dto = new ImageRecognitionRequestDTO(new Image(request.Base64));
            dto.Features.Add(new Feature("LABEL_DETECTION"));
            dto.Features.Add(new Feature("IMAGE_PROPERTIES"));
            var requestDto = new GoogleVisionRequest();
            requestDto.Requests.Add(dto);
            var visionDto = await service.GetGoogleVisionResponseAsync(requestDto);
            return classificationService.ConvertToClassificationDto(visionDto);
        }

        static async Task<string> MakeGoogleRequest()
        {
            using (var client = new HttpClient())
            {
                var payload = System.IO.File.ReadAllText(@"C:\Users\beata\OneDrive\Pulpit\vision.json");
                const string key = "AIzaSyBf6l9N5cvfsr4NsOU-SSkXOlHiyYVs6_o";
                var url =
                    $"https://vision.googleapis.com/v1/images:annotate?key={key}";

                var httpContent = new StringContent(payload);

                var response = await client.PostAsync(url, httpContent);
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
