using System.Collections.Generic;
using System.Threading.Tasks;
using EcoEye.DTO;
using EcoEye.DTO.Google.Response;

namespace EcoEye.Service.Classification
{
    public class ClassificationService : IClassificationService
    {
        public ImageClassificationResponseDTO ConvertToClassificationDto(GoogleVisionResponse googleVisionResponse)
        {
            if (googleVisionResponse.Responses.Count == 0)
                return null;
            var labelAnnotations = googleVisionResponse.Responses[0].LabelAnnotations;
            if (labelAnnotations.Count == 0)
                return null;
            var topMaterial = GetTopMaterialType(labelAnnotations);
            var rubbishBinType = GetRubbishBinType(topMaterial);
            var minDecompositionMonths = GetMinDecompositionMonths(topMaterial);
            var maxDecompositionMonths = GetMaxDecompositionMonths(topMaterial);
            var convertedDto = new ImageClassificationResponseDTO
            {
                MaterialType = topMaterial,
                RubbishBinColor = rubbishBinType,
                DecompositionMax = maxDecompositionMonths,
                DecompositionMin = minDecompositionMonths
            };

            return convertedDto;
        }

        private MaterialType GetMaterialType(LabelAnnotation labelAnnotation)
        {
            var desc = labelAnnotation.Description.ToUpper();
            if (desc.Contains("FOOD") || desc.Contains("DISHES"))
                return MaterialType.BIO;
            if (desc.Contains("GLASS"))
                return MaterialType.GLASS;
            if (desc.Contains("PAPER"))
                return MaterialType.PAPER;
            if (desc.Contains("PLASTIC"))
                return MaterialType.PLASTIC;
            if (desc.Contains("METAL"))
                return MaterialType.METAL;
            if (desc.Contains("ELECTRONIC"))
                return MaterialType.ELECTRONIC;

            return MaterialType.OTHER;
        }

        private MaterialType GetTopMaterialType(List<LabelAnnotation> annotations)
        {
            MaterialType totalType = MaterialType.OTHER;
            int totalTypeCount = 0;
            var dict = new Dictionary<MaterialType, int>();
            dict.Add(MaterialType.BIO, 0);
            dict.Add(MaterialType.ELECTRONIC, 0);
            dict.Add(MaterialType.GLASS, 0);
            dict.Add(MaterialType.METAL, 0);
            dict.Add(MaterialType.PAPER, 0);
            dict.Add(MaterialType.PLASTIC, 0);
            foreach(var annotation in annotations)
            {
                MaterialType type = GetMaterialType(annotation);
                if (type == MaterialType.OTHER) continue;
                dict[type]++;
                if (dict[type] >= totalTypeCount)
                {
                    totalType = type;
                    totalTypeCount = dict[type];
                }
            }

            return totalType;
        }

        private RubbishBinType GetRubbishBinType(MaterialType type)
        {
            switch (type)
            {
                case MaterialType.PLASTIC:
                    return RubbishBinType.YELLOW;
                case MaterialType.GLASS:
                    return RubbishBinType.GREEN;
                case MaterialType.BIO:
                    return RubbishBinType.BROWN;
                case MaterialType.ELECTRONIC:
                    return RubbishBinType.ELECTRONIC;
                case MaterialType.PAPER:
                    return RubbishBinType.BLUE;
                case MaterialType.OTHER:
                    return RubbishBinType.BLACK;
            }

            return RubbishBinType.BLACK;
        }

        private float GetMinDecompositionMonths(MaterialType type)
        {
            switch (type)
            {
                case MaterialType.PLASTIC:
                    return 1200.0f;
                case MaterialType.GLASS:
                    return 48000.0f;
                case MaterialType.BIO:
                    return 2.0f;
                case MaterialType.METAL:
                case MaterialType.ELECTRONIC:
                    return 120.0f;
                case MaterialType.PAPER:
                    return 3.0f;
                case MaterialType.OTHER:
                    return 2.0f;
            }

            return 0.0f;
        }

        private float GetMaxDecompositionMonths(MaterialType type)
        {
            switch (type)
            {
                case MaterialType.PLASTIC:
                    return 12000.0f;
                case MaterialType.GLASS:
                    return 48000.0f;
                case MaterialType.BIO:
                    return 12.0f;
                case MaterialType.METAL:
                case MaterialType.ELECTRONIC:
                    return 2400.0f;
                case MaterialType.PAPER:
                    return 6.0f;
                case MaterialType.OTHER:
                    return 2.0f;
            }

            return 0.0f;
        }
    }
}