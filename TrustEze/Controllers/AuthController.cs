using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;
using TrustEze.Models;

namespace TrustEze.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<AuthController> _logger;
        private readonly AuthControllerOptions _options;

        public AuthController(ILogger<AuthController> logger, IHttpClientFactory httpClientFactory, IOptions<AuthControllerOptions> options)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _options = options.Value;
        }
        [HttpGet("")]
        public async Task<IActionResult> Auth([FromQuery] string code)
        {

            //            curl--location--request POST 'https://trusteze.auth.us-east-1.amazoncognito.com/oauth2/token' \
            //--header 'Content-Type: application/x-www-form-urlencoded' \
            //--data - urlencode 'code=86b710fd-c79a-4b64-a05d-2b557c963e35' \
            //--data - urlencode 'grant_type=authorization_code' \
            //--data - urlencode 'client_id=3gn8kb63bta2jmkjhcnhtnb4q0' \
            //--data - urlencode 'client_secret=1ben1edl158vsaa276fhflpko952ca42af7s94b9nd40diooi1g9' \
            //--data - urlencode 'redirect_uri=http://localhost:80/auth'

            //var cs = "1ben1edl158vsaa276fhflpko952ca42af7s94b9nd40diooi1g9";
            //var clientId = "3gn8kb63bta2jmkjhcnhtnb4q0";
            //var redirectUri = "http://localhost:80/auth";
            //var cognitoUri = "https://trusteze.auth.us-east-1.amazoncognito.com/oauth2/token";

            var k = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("client_id", _options.ClientId),
                new KeyValuePair<string, string>("client_secret", _options.ClientSecret),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("redirect_uri", _options.CallbackUri),
            };
            var content = new FormUrlEncodedContent(k);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, _options.CognitoUri)
            {
                Content = content
            };
            httpRequestMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");

            var httpClient = _httpClientFactory.CreateClient();

            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var json = JsonSerializer.Deserialize<object>(await httpResponseMessage.Content.ReadAsStringAsync());
                return new JsonResult(json);
            }

            return new BadRequestResult();


        }

        [HttpGet("callback")]
        public IActionResult Callback()
        {
            return Redirect("https://trusteze.auth.us-east-1.amazoncognito.com/login?client_id=3gn8kb63bta2jmkjhcnhtnb4q0&response_type=token&scope=email+openid+phone&redirect_uri=http%3A%2F%2Flocalhost%3A80%2Fauth");
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return Redirect("https://trusteze.auth.us-east-1.amazoncognito.com/login?client_id=3gn8kb63bta2jmkjhcnhtnb4q0&response_type=code&scope=email+openid+phone&redirect_uri=http%3A%2F%2Flocalhost%3A80%2Fauth");
        }
    }
}