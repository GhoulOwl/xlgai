using System;

namespace AutoStardew.Tasks;

/// <summary>
/// Harvests any mature crops available on the farm.
/// </summary>
public class HarvestCropsTask : ITask
{
    public bool CanExecute(GameState state) => state.HasHarvestableCrops;

    public float Cost(GameState state) => 2f;

    public GameState Apply(GameState state) => state with { HasHarvestableCrops = false, Energy = state.Energy - 10 };

    public void Execute()
    {
        Console.WriteLine("[AutoStardew] Harvesting crops...");
    }
}

