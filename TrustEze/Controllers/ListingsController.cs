using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text.Json;
using TrustEze.Models;
using TrustEze.Models.Api;

namespace TrustEze.Controllers
{
    /// <summary>
    /// Testing Summary Goes Here
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ListingsController : ControllerBase
    {
        private static readonly Root? _listingResponse;
        static ListingsController()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "TrustEze.listingSample.json";
            using (Stream? stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream ?? throw new Exception("TrustEze.listingSample.json not found")))
            {
                string result = reader.ReadToEnd();
                _listingResponse = JsonSerializer.Deserialize<Root>(result);
                if (_listingResponse == null)
                {
                    throw new Exception("Error Deserializing 'TrustEze.listingSample.json'");
                }
            }
        }

        private readonly ILogger<AccountsController> _logger;

        public ListingsController(ILogger<AccountsController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Get Listings comment goes here
        /// </summary>
        /// <returns>returns a thing</returns>
        [HttpGet(Name = "GetListings")]
        public Root Get([FromQuery] SampleParam param, [FromBody] SampleBody body)
        {
            return _listingResponse;
        }
    }
}
/// <summary>
/// SampleParam comment goes here
/// </summary>
public class SampleParam
{
    /// <summary>
    /// id comment goes here
    /// </summary>
    public int Id { get; set; }
}
/// <summary>
/// SampleBody comment goes here
/// </summary>
public class SampleBody
{
    /// <summary>
    /// name comment goes here
    /// </summary>
    public string Name { get; set; }
}