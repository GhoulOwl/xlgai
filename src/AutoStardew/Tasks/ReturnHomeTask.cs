using System;

namespace AutoStardew.Tasks;

/// <summary>
/// Returns the farmer home when it's late or energy is low.
/// </summary>
public class ReturnHomeTask : ITask
{
    public bool CanExecute(GameState state) => state.TimeOfDay >= 2130 || state.Energy <= 20;

    public float Cost(GameState state) => 100f; // ensure executed last

    public GameState Apply(GameState state) => state;

    public void Execute()
    {
        Console.WriteLine("[AutoStardew] Heading home and going to bed...");
    }
}

