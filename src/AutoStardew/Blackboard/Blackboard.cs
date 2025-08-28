namespace AutoStardew.Blackboard;

/// <summary>
/// Simple key/value store used to share state between planning components.
/// </summary>
public class Blackboard
{
    private readonly Dictionary<string, object> _data = new();

    public T? Get<T>(string key)
    {
        if (_data.TryGetValue(key, out var value) && value is T t)
            return t;
        return default;
    }

    public void Set<T>(string key, T value) => _data[key] = value!;
}
