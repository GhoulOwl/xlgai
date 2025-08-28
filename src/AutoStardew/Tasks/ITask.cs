namespace AutoStardew.Tasks;

/// <summary>
/// Basic task interface for automation actions.
/// </summary>
public interface ITask
{
    bool CanExecute(GameState state);
    float Cost(GameState state);
    GameState Apply(GameState state);
    void Execute();
}

/// <summary>
/// Simplified snapshot of relevant game information used for planning.
/// </summary>
/// <param name="Day">Current in-game day number.</param>
/// <param name="TimeOfDay">Current time as minutes since 6:00AM.</param>
/// <param name="Energy">Player's remaining energy.</param>
/// <param name="IsRaining">Whether it is currently raining.</param>
/// <param name="HasHarvestableCrops">True if there are mature crops ready to harvest.</param>
/// <param name="HasSeedsToPlant">True if there are seeds and tilled soil available.</param>
/// <param name="InventoryFull">True if the player's inventory is full.</param>
public record GameState(
    int Day,
    int TimeOfDay,
    int Energy,
    bool IsRaining,
    bool HasHarvestableCrops,
    bool HasSeedsToPlant,
    bool InventoryFull);
