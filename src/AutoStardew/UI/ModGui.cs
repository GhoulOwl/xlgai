namespace AutoStardew.UI;

/// <summary>
/// Placeholder configuration menu. In-game this would render a proper UI
/// but for the purposes of this repository it simply stores the
/// configured values.
/// </summary>
public class ModGui
{
    private readonly ModConfig _config;

    public bool IsOpen { get; private set; }

    public ModGui(ModConfig config) => _config = config;

    public void Toggle() => IsOpen = !IsOpen;

    /// <summary>
    /// Render the GUI. For now this just writes the current configuration
    /// to the console so unit tests can verify behaviour.
    /// </summary>
    public void Draw()
    {
        if (!IsOpen) return;
        Console.WriteLine("=== AutoStardew Config ===");
        Console.WriteLine($"Endpoint: {_config.ApiEndpoint}");
        Console.WriteLine($"API Key: {_config.ApiKey}");
        Console.WriteLine($"Model: {_config.Model}");
    }

    public void SetApiEndpoint(string url) => _config.ApiEndpoint = url;
    public void SetApiKey(string key) => _config.ApiKey = key;
    public void SetModel(string model) => _config.Model = model;
}
