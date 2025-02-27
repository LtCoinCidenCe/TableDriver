using System.Reflection;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace TableDriver.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(ILogger<AuthController> logger) : ControllerBase
{
    public class TokenRequest
    {
        public string code { get; set; } = string.Empty;
        public string grant_type { get; set; } = "authorization_code";
        public string redirect_uri { get; set; } = REDIRECT_URI; // this is required or else you don't get token.
        public string client_id { get; set; } = APP_KEY;
        public string client_secret { get; set; } = string.Empty;

        public Dictionary<string, string> ToDictionary()
        {
            return new Dictionary<string, string>()
            {
                {"code",code},
                {"grant_type",grant_type},
                {"redirect_uri",redirect_uri},
                {"client_id",client_id},
                {"client_secret",client_secret}
            };
        }

        public Dictionary<string, string?> ToDictionaryR()
        {
            return GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).ToDictionary(prop => prop.Name, prop => (string?)prop.GetValue(this, null));
        }
    }

    public class TokenResponse
    {
        public string access_token { get; set; } = string.Empty;
        public int expires_in { get; set; }
        public string token_type { get; set; } = string.Empty;
        public string scope { get; set; } = string.Empty;
        public string account_id { get; set; } = string.Empty;
        public string team_id { get; set; } = string.Empty;
        public string refresh_token { get; set; } = string.Empty;
        public string id_token { get; set; } = string.Empty;
        public string uid { get; set; } = string.Empty;
    }

    [HttpGet]
    public async Task<IActionResult> ReceiveAuthCode([FromQuery] string code, [FromQuery] string? state)
    {
        var tokenRequest = new TokenRequest()
        {
            code = code,
            client_secret = APP_SECRET,
        };
        Dictionary<string, string> payload = tokenRequest.ToDictionary();
        logger.LogInformation(JsonSerializer.Serialize(tokenRequest.ToDictionaryR()));

        FormUrlEncodedContent formUrlEncodedContent = new(payload);
        HttpResponseMessage postA = await httpClient.PostAsync(DROPBOX_TOKEN, formUrlEncodedContent);

        string stringResult = await postA.Content.ReadAsStringAsync();
        logger.LogInformation(stringResult);

        TokenResponse? tokenResponse = await postA.Content.ReadFromJsonAsync<TokenResponse>();
        return Ok(tokenResponse);
    }

    [HttpGet("uri")]
    public string GetTheWebURI()
    {
        const string beforeExtension = "https://www.dropbox.com/oauth2/authorize?client_id=5x6rdzdy05urbfe&response_type=code&redirect_uri=";
        return beforeExtension + Uri.EscapeDataString(REDIRECT_URI);
    }

    public const string APP_KEY = "5x6rdzdy05urbfe";
    public const string APP_SECRET = "peknv0iotkj346r";
    public const string REDIRECT_URI = "http://localhost:5192/api/auth";
    public const string DROPBOX_TOKEN = "https://api.dropboxapi.com/oauth2/token";

    public static HttpClient httpClient = new HttpClient() { Timeout = TimeSpan.FromSeconds(20) };
}
