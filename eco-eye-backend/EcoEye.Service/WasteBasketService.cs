using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcoEye.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EcoEye.Service
{
    public class WasteBasketService : IWasteBasketService
    {
        public byte[] ConvertData(string fileName)
        {
            var jsonTeitemt = File.ReadAllText(fileName);
            var jsonArrayOfObjects = JObject.Parse(jsonTeitemt);
            var results = new List<WasteBasketDTO>();

            foreach (var item in jsonArrayOfObjects.GetValue("properties"))
            {
                results.Add(new WasteBasketDTO
                {
                    Batteries = YesNoToBoolean(item["recycling:batteries"].Value<string>()),
                    Books = YesNoToBoolean(item["recycling:books"].Value<string>()),
                    Cans = YesNoToBoolean(item["recycling:cans"].Value<string>()),
                    CardBoard = YesNoToBoolean(item["recycling:cardBoard"].Value<string>()),
                    Cartons = YesNoToBoolean(item["recycling:cartons"].Value<string>()),
                    Clothes = YesNoToBoolean(item["recycling:clothes"].Value<string>()),
                    ElectricalAppliances = YesNoToBoolean(item["recycling:electrical_appliances"].Value<string>()),
                    Wood = YesNoToBoolean(item["recycling:wood"].Value<string>()),
                    Glass = YesNoToBoolean(item["recycling:glass"].Value<string>()),
                    GlassBottles = YesNoToBoolean(item["recycling:glass_bottles"].Value<string>()),
                    GreenWaste = YesNoToBoolean(item["recycling:green_waste"].Value<string>()),
                    Magazines = YesNoToBoolean(item["recycling:magazines"].Value<string>()),
                    Newspaper = YesNoToBoolean(item["recycling:newspaper"].Value<string>()),
                    Waste = YesNoToBoolean(item["recycling:waste"].Value<string>()),
                    PlasticBottles = YesNoToBoolean(item["recycling:plastic_bottles"].Value<string>()),
                    Paper = YesNoToBoolean(item["recycling:paper"].Value<string>()),
                    Plastic = YesNoToBoolean(item["recycling:plastic"].Value<string>()),
                    PaperPackaging = YesNoToBoolean(item["recycling:paper_packaging"].Value<string>()),
                    ScrapMetal = YesNoToBoolean(item["recycling:paper_packaging"].Value<string>()),
                    SmallAppliances = YesNoToBoolean(item["recycling:small_appliances"].Value<string>()),
                    PlasticPackaging = YesNoToBoolean(item["recycling:plastic_packaging"].Value<string>()),
                });
            }

            // WIP

            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(results));
        }

        public async Task<IList<WasteBasketDTO>> GetWasteBasketsAsync()
        {
            throw new System.NotImplementedException();
        }

        private bool? YesNoToBoolean(string value)
        {
            if (string.IsNullOrEmpty(value) || value is string s && s == "no")
            {
                return null;
            }

            return value == "yes";
        }
    }
}