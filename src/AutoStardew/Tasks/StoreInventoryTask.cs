using System;

namespace AutoStardew.Tasks;

/// <summary>
/// Deposits items from the player's inventory into nearby chests when full.
/// </summary>
public class StoreInventoryTask : ITask
{
    public bool CanExecute(GameState state) => state.InventoryFull;

    public float Cost(GameState state) => 0.5f;

    public GameState Apply(GameState state) => state with { InventoryFull = false };

    public void Execute()
    {
        Console.WriteLine("[AutoStardew] Storing items to chest...");
    }
}

