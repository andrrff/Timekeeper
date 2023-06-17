using System.Text;
using System.Net.Http.Headers;

namespace Timekeeper.Infrastructure.ExternalServices.Jira;

public class JiraClient
{
    private readonly string _instance;
    private readonly HttpClient _httpClient;

    private string? Endpoint { get; set;} = "search";

    public JiraClient(string username, string password, string instance)
    {
        _instance   = instance;

        var bytesAuth  = Encoding.ASCII.GetBytes($"{username}:{password}");
        var authHeader = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytesAuth));

        _httpClient = new HttpClient
        {
            DefaultRequestHeaders = {
                Authorization = authHeader
            }
        };
    }

    private string Url(string query = "") => $"https://{_instance}.atlassian.net/rest/api/3/{Endpoint}?{query}";

    public async Task<string> GetSearchAsync(string query = "")
    {
        var response = await _httpClient.GetAsync(Url(query));

        return await response.Content.ReadAsStringAsync();
    }	
}