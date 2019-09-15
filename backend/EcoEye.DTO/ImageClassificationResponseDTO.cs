namespace EcoEye.DTO
{
    public class ImageClassificationResponseDTO
    {
        public MaterialType MaterialType { get; set; }
        public RubbishBinType RubbishBinColor { get; set; }
        public float DecompositionMin { get; set; }
        public float DecompositionMax { get; set; }
    }
}
