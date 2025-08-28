using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace AutoStardew;

/// <summary>
/// Minimal wrapper around an OpenAI-compatible chat completion API.
/// The base URL and API key are taken from <c>OPENAI_API_BASE</c> and
/// <c>OPENAI_API_KEY</c> environment variables.
/// </summary>
public class AiPlanner
{
    private readonly HttpClient _client;

    public AiPlanner(HttpClient? client = null)
    {
        _client = client ?? new HttpClient();
        var baseUrl = Environment.GetEnvironmentVariable("OPENAI_API_BASE") ?? "https://api.openai.com";
        _client.BaseAddress = new Uri(baseUrl);

        var apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
        if (!string.IsNullOrEmpty(apiKey))
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
    }

    /// <summary>
    /// Sends a chat completion request and returns the assistant message content.
    /// </summary>
    public async Task<string> GetPlanAsync(string prompt, CancellationToken ct = default)
    {
        var request = new
        {
            model = Environment.GetEnvironmentVariable("OPENAI_MODEL") ?? "gpt-3.5-turbo",
            messages = new[] { new { role = "user", content = prompt } }
        };

        using var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
        using var response = await _client.PostAsync("/v1/chat/completions", content, ct);
        response.EnsureSuccessStatusCode();
        using var stream = await response.Content.ReadAsStreamAsync(ct);
        using var doc = await JsonDocument.ParseAsync(stream, cancellationToken: ct);
        return doc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString() ?? string.Empty;
    }
}
