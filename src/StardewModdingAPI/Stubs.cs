namespace StardewModdingAPI;

public abstract class Mod
{
    public IModHelper Helper => throw new System.NotImplementedException();
    public abstract void Entry(IModHelper helper);
}

public interface IModHelper
{
    IInputHelper Input { get; }
    IEventHelper Events { get; }
}

public interface IInputHelper
{
    bool IsDown(SButton button);
}

public interface IEventHelper
{
    IGameLoopEvents GameLoop { get; }
}

public interface IGameLoopEvents
{
    event System.EventHandler<UpdateTickedEventArgs> UpdateTicked;
}

public class UpdateTickedEventArgs : System.EventArgs
{
}

public enum SButton
{
    O,
    P
}