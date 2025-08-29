using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace AutoStardew;

/// <summary>
/// Minimal wrapper around an OpenAI-compatible chat completion API.
///
/// The endpoint, API key and model can be configured at runtime. If no
/// values are supplied the constructor falls back to environment
/// variables (<c>OPENAI_API_BASE</c>, <c>OPENAI_API_KEY</c> and
/// <c>OPENAI_MODEL</c>).
/// </summary>
public class AiPlanner
{
    private readonly HttpClient _client;

    /// <summary>Base URL for the chat completion API.</summary>
    public string ApiBase { get; private set; }

    /// <summary>Bearer token used for authentication.</summary>
    public string ApiKey { get; private set; }

    /// <summary>Model identifier sent in the request body.</summary>
    public string Model { get; private set; }

    public AiPlanner(HttpClient? client = null)
    {
        _client = client ?? new HttpClient();

        ApiBase = Environment.GetEnvironmentVariable("OPENAI_API_BASE") ?? "https://api.openai.com";
        _client.BaseAddress = new Uri(ApiBase);

        ApiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY") ?? string.Empty;
        if (!string.IsNullOrEmpty(ApiKey))
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);

        Model = Environment.GetEnvironmentVariable("OPENAI_MODEL") ?? "gpt-3.5-turbo";
    }

    /// <summary>
    /// Update authentication or endpoint information at runtime. Any
    /// parameter left <c>null</c> retains its current value.
    /// </summary>
    public void Configure(string? apiBase = null, string? apiKey = null, string? model = null)
    {
        if (!string.IsNullOrWhiteSpace(apiBase) && apiBase != ApiBase)
        {
            ApiBase = apiBase;
            _client.BaseAddress = new Uri(ApiBase);
        }

        if (apiKey is not null && apiKey != ApiKey)
        {
            ApiKey = apiKey;
            if (string.IsNullOrEmpty(ApiKey))
                _client.DefaultRequestHeaders.Authorization = null;
            else
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);
        }

        if (!string.IsNullOrWhiteSpace(model))
            Model = model;
    }

    /// <summary>
    /// Sends a chat completion request and returns the assistant message content.
    /// </summary>
    public async Task<string> GetPlanAsync(string prompt, CancellationToken ct = default)
    {
        var request = new
        {
            model = Model,
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
