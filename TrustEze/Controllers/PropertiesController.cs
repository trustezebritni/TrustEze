using Microsoft.AspNetCore.Mvc;
using TrustEze.Models;

namespace TrustEze.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PropertiesController : ControllerBase
    {
        private readonly List<PropertyDto> _properties = new List<PropertyDto>
        {

            new PropertyDto
            {
                Id = Guid.NewGuid(),
                Address = "9876 E central ave. sacramento, CA 95838",
                Demographics = new Dictionary<string, string>
                {
                    { "", ""}
                }
            },
            new PropertyDto
            {
                Id = Guid.NewGuid(),
                Address = "1234 main st. LA, CA 85444",
                Demographics = new Dictionary<string, string>
                {
                    { "", ""}
                }
            },
            new PropertyDto
            {
                Id = Guid.NewGuid(),
                Address = "2468 w. north way PHX, AZ 85266",
                Demographics = new Dictionary<string, string>
                {
                    { "", ""}
                }
            },
        };
        private readonly ILogger<AccountsController> _logger;

        public PropertiesController(ILogger<AccountsController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetProperties")]
        public IEnumerable<PropertyDto> Get()
        {
            return _properties;
        }
    }
}