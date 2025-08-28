using System;

namespace AutoStardew.Tasks;

/// <summary>
/// Plants seeds on any available tilled soil.
/// </summary>
public class PlantSeedsTask : ITask
{
    public bool CanExecute(GameState state) => state.HasSeedsToPlant;

    public float Cost(GameState state) => 3f;

    public GameState Apply(GameState state) => state with { HasSeedsToPlant = false, Energy = state.Energy - 15 };

    public void Execute()
    {
        Console.WriteLine("[AutoStardew] Planting seeds...");
    }
}

