using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcoEye.DTO;
using EcoEye.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace EcoEye.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WasteBasketController : ControllerBase
    {
        public IWasteBasketService WasteBasketService { get; set; }

        public WasteBasketController()
        {
            WasteBasketService = new WasteBasketService();
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return new JsonResult(await WasteBasketService.GetWasteBasketsAsync());
        }

        [HttpPost]
        public IActionResult ConvertData([FromBody] ConvertDataRequestDTO request)
        {
            var fileName =
                WasteBasketService.ConvertData(request.FilePath);

            return File(fileName, "application/json");
        }
    }
}