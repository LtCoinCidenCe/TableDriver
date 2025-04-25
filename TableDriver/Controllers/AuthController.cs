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
        /// <summary>
        /// <para>Finally You have the access_token on TableDriver for your files in DropBox.</para>
        /// <para>Now goto OAUTH.http and paste the access_token to retrieve anything from your account.</para>
        /// </summary>
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
        // The user has agreed that this app TableDriver can access my data on DropBox (done in webBrowser).
        // So string code is the proof
        var tokenRequest = new TokenRequest()
        {
            code = code,
            client_secret = APP_SECRET,
        };
        Dictionary<string, string> payload = tokenRequest.ToDictionary();
        logger.LogInformation(JsonSerializer.Serialize(tokenRequest.ToDictionaryR()));
        FormUrlEncodedContent formUrlEncodedContent = new(payload);
        // The payload formUrlEncodedContent is created.

        HttpResponseMessage postA = await httpClient.PostAsync(DROPBOX_TOKEN_URL, formUrlEncodedContent);

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

    /// <summary>
    /// Requested from DropBox. A representation of our TableDriver in DropBox.
    /// </summary>
    public const string APP_KEY = "5x6rdzdy05urbfe";
    /// <summary>
    /// <para>After DropBox authorization. I want the user browser to go back to ReceiveAuthCode (or a webpage of TableDriver)</para>
    /// <para>You see there at ReceiveAuthCode it takes the code, so it needs to be encrypted in https</para>
    /// <para>Or localhost, where your browser talks to localhost so no security issue.
    /// </summary>
    public const string REDIRECT_URI = "http://localhost:5192/api/auth";
    public const string DROPBOX_TOKEN_URL = "https://api.dropboxapi.com/oauth2/token";

    // confidential
    public const string APP_SECRET = "peknv0iotkj346r";

    public static HttpClient httpClient = new HttpClient() { Timeout = TimeSpan.FromSeconds(20) };
}
