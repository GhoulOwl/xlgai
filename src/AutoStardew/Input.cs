namespace AutoStardew;

/// <summary>
/// Very small input abstraction used only for unit testing the mod logic.
/// It records which keys are currently being pressed.
/// </summary>
[Flags]
public enum Keys
{
    None = 0,
    O = 1 << 0,
    P = 1 << 1,
}

public readonly record struct InputState(Keys Pressed)
{
    public bool IsPressed(Keys combo) => (Pressed & combo) == combo;
}
