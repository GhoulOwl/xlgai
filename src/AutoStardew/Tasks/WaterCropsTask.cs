using System;

namespace AutoStardew.Tasks;

/// <summary>
/// Waters any dry crops on the farm. This is skipped on rainy days.
/// </summary>
public class WaterCropsTask : ITask
{
    public bool CanExecute(GameState state) => !state.IsRaining;

    public float Cost(GameState state) => 1f;

    public GameState Apply(GameState state) => state with { Energy = state.Energy - 20 };

    public void Execute()
    {
        Console.WriteLine("[AutoStardew] Watering crops...");
    }
}

