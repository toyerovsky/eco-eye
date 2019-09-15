namespace EcoEye.DTO
{
    public class WasteBasketDTO
    {
        public string BuildingLevels { get; set; }
        public bool? Batteries { get; set; }
        public bool? Books { get; set; }
        public bool? Cans { get; set; }
        public bool? CardBoard { get; set; }
        public bool? Cartons { get; set; }
        public bool? Clothes { get; set; }
        public bool? ElectricalAppliances { get; set; }
        public bool? Glass { get; set; }
        public bool? GlassBottles { get; set; }
        public bool? GreenWaste { get; set; }
        public bool? Magazines { get; set; }
        public bool? Newspaper { get; set; }
        public bool? Paper { get; set; }
        public bool? PaperPackaging { get; set; }
        public bool? Plastic { get; set; }
        public bool? PlasticBottles { get; set; }
        public bool? PlasticPackaging { get; set; }
        public bool? ScrapMetal { get; set; }
        public bool? SmallAppliances { get; set; }
        public bool? Waste { get; set; }
        public bool? Wood { get; set; }
        public decimal Lat { get; set; }
        public decimal Lon { get; set; }
        public string Location { get; set; }
        public WasteBasketType WasteBasketType { get; set; }
        public string Color { get; set; }
    }
}