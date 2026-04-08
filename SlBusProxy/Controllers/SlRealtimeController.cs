using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;

[ApiController]
[Route("api/[controller]")]
public class SlRealtimeController : ControllerBase
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly SlSettings _settings;

    public SlRealtimeController(
        IHttpClientFactory clientFactory,
        IOptions<SlSettings> options)
    {
        _clientFactory = clientFactory;
        _settings = options.Value;
    }

    /// <summary>
    /// Proxy endpoint for SL real-time departures from Gullmarsplan.
    /// GET /api/slrealtime/departures?timeWindow=60
    /// </summary>
    [HttpGet("departures")]
    public async Task<IActionResult> GetDepartures([FromQuery] int timeWindow = 60)
    {
        var client = _clientFactory.CreateClient("SlClient");

        // Build SL URL: adjust for different version or params
        var url =
            $"trip?format=json&passlist=true&showPassingPoints=true" +
            $"&originId={WebUtility.UrlEncode(_settings.OriginSiteId)}" +
            $"&destId={WebUtility.UrlEncode(_settings.DestinationSiteId)}" +
            $"&accessId=de08d6c2-a598-4311-817e-ae0fc1b1a0ca";

        var response = await client.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var errorBody = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, new
            {
                error = "Error calling SL API",
                status = (int)response.StatusCode,
                body = errorBody
            });
        }

        // Just pass through JSON from SL
        var content = await response.Content.ReadAsStringAsync();
        return Content(content, "application/json");
    }
}