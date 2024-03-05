using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace YourMatchTgBot.Services;

public class TranslateService
{
    private const string TRANSLATE_URI = "https://ftapi.pythonanywhere.com//translate?sl={1}&dl={2}&text={0}";
    
    private static readonly HttpClient _httpClient = new HttpClient();
    
    public static async Task<string> TranslateText(string input, string from, string to, CancellationToken stoppingToken)
    {
        var request = new HttpRequestMessage();
        request.Headers.Add("user-agent",
            "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
        request.Headers.Add("Referer", "http://www.microsoft.com");
        request.RequestUri = new Uri(string.Format(TRANSLATE_URI, input, from, to));

        var response = await _httpClient.SendAsync(request, stoppingToken);

        var json = await response.Content.ReadAsStringAsync(stoppingToken);

        var obj = JsonConvert.DeserializeObject(json) ?? throw new InvalidOperationException();

        if (obj is not JObject jObject)
            throw new InvalidOperationException();
        
        return jObject["destination-text"].Value<string>();
    }
}