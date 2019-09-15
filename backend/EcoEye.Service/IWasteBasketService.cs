using System.Collections.Generic;
using System.Threading.Tasks;
using EcoEye.DTO;

namespace EcoEye.Service
{
    public interface IWasteBasketService
    {
        byte[] ConvertData(string fileName);
        Task<IList<WasteBasketDTO>> GetWasteBasketsAsync();
    }
}