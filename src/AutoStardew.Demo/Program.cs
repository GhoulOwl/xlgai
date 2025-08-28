using AutoStardew;
using AutoStardew.Tasks;

// Simple console demo showing the MVP task flow.
var mod = new ModEntry();

var state = new GameState(
    Day: 1,
    TimeOfDay: 2200,
    Energy: 270,
    IsRaining: false,
    HasHarvestableCrops: true,
    HasSeedsToPlant: true,
    InventoryFull: false);

mod.SetupMvpTasks(state);
mod.Run(state);
