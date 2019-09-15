using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using EcoEye.DTO.Fact;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EcoEye.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FactController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private List<string> facts = new List<string>();

        public FactController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            InitFacts();
        }

        [HttpGet("random")]
        public ActionResult<FactDto> GetRandomFact()
        {
            var randomIndex = new Random().Next(0, facts.Count - 1);
            return new FactDto {Content = facts[randomIndex]};
        }

        [HttpGet("all")]
        public ActionResult<IEnumerable<FactDto>> GetAllFascts()
        {
            List<FactDto> dto = new List<FactDto>();
            foreach (var fact in facts)
            {
                dto.Add(new FactDto {Content = fact});
            }
            return dto;
        }

        private void InitFacts()
        {

            string contentRootPath = _hostingEnvironment.ContentRootPath;
            var JSON = System.IO.File.ReadAllText(contentRootPath + "/Files/facts.json");
            facts = JsonConvert.DeserializeObject<List<string>>(JSON);
        }

    }
}