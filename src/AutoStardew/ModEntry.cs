using AutoStardew.Blackboard;
using AutoStardew.Tasks;
using AutoStardew.UI;
using StardewModdingAPI;

namespace AutoStardew;

/// <summary>
///     SMAPI mod entry point which wires up the scheduler and a minimal config GUI.
/// </summary>
public class ModEntry : Mod
{
    private readonly ModConfig _config = new();
    private readonly AiPlanner _planner = new();
    private readonly TaskQueue _scheduler = new();
    private readonly Blackboard.Blackboard _blackboard = new();
    private ModGui? _gui;

    public override void Entry(IModHelper helper)
    {
        _gui = new ModGui(_config);
        _planner.Configure(_config.ApiEndpoint, _config.ApiKey, _config.Model);
        
        helper.Events.GameLoop.UpdateTicked += OnUpdateTicked;
    }

    /// <summary>
    ///     Builds a very naive daily plan using the AI planner. The prompt is intentionally
    ///     simple and purely demonstrative.
    /// </summary>
    public async Task<string> BuildDailyPlanAsync(string weather)
    {
        var prompt = $"Plan farming tasks for a {weather} day in Stardew Valley.";
        return await _planner.GetPlanAsync(prompt);
    }

    /// <summary>Enqueue a task into the scheduler.</summary>
    public void EnqueueTask(ITask task) => _scheduler.Enqueue(task);

    /// <summary>
    ///     Enqueue a minimal set of tasks that cover the MVP flow of watering,
    ///     harvesting, planting, inventory management and returning home.
    /// </summary>
    public void SetupMvpTasks(GameState state)
    {
        if (state.InventoryFull)
            EnqueueTask(new StoreInventoryTask());

        if (!state.IsRaining)
            EnqueueTask(new WaterCropsTask());

        if (state.HasHarvestableCrops)
            EnqueueTask(new HarvestCropsTask());

        if (state.HasSeedsToPlant)
            EnqueueTask(new PlantSeedsTask());

        EnqueueTask(new ReturnHomeTask());
    }

    /// <summary>Runs queued tasks against the provided game state snapshot.</summary>
    public void Run(GameState state) => _scheduler.Execute(state);

    private void OnUpdateTicked(object? sender, UpdateTickedEventArgs e)
    {
        if (_gui is null)
            return;

        if (Helper.Input.IsDown(SButton.O) && Helper.Input.IsDown(SButton.P))
            _gui.Toggle();

        if (_gui.IsOpen)
            _gui.Draw();
    }
}
