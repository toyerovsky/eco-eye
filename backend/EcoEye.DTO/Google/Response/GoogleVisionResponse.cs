using System.Collections.Generic;
namespace EcoEye.DTO.Google.Response
{
    public class GoogleVisionResponse
    {
        public List<LabelList> Responses { get; set; } = new List<LabelList>();
    }
}
