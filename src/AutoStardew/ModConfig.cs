namespace AutoStardew;

/// <summary>
/// Simple configuration container that holds API endpoint information.
/// In the real mod this would be persisted to disk and edited through a
/// proper UI. For now it only lives in memory.
/// </summary>
public class ModConfig
{
    /// <summary>Base URL for the AI chat completion API.</summary>
    public string ApiEndpoint { get; set; } = "https://api.link-ai.tech";

    /// <summary>Key used for authenticating requests.</summary>
    public string ApiKey { get; set; } = string.Empty;

    /// <summary>Model identifier provided by the provider.</summary>
    public string Model { get; set; } = "LinkAI-4.1";
}
