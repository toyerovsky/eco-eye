using System.Collections.Generic;
namespace EcoEye.DTO.Google
{
    public class ImageRecognitionRequestDTO
    {
        public List<Feature> Features { get; set; }
        public Image Image { get; set; }

        public ImageRecognitionRequestDTO(Image image)
        {
            Image = image;
            Features = new List<Feature>();
        }
    }
}
