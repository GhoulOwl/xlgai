using AutoStardew.Blackboard;
using AutoStardew.Tasks;
using AutoStardew.UI;

namespace AutoStardew;

/// <summary>
/// Entry point placeholder for the automation mod. In a real SMAPI mod this
/// would derive from <c>Mod</c> and hook into game events.
/// </summary>
public class ModEntry
{ 
    private readonly ModConfig _config = new();
    private readonly AiPlanner _planner = new();
    private readonly TaskQueue _scheduler = new();
    private readonly Blackboard.Blackboard _blackboard = new();
    private readonly ModGui _gui;

    public ModEntry()
    {
        _gui = new ModGui(_config);
        // Apply configuration to planner at startup
        _planner.Configure(_config.ApiEndpoint, _config.ApiKey, _config.Model);
    }

    /// <summary>
    /// Builds a very naive daily plan using the AI planner. The prompt is intentionally
    /// simple and purely demonstrative.
    /// </summary>
    public async Task<string> BuildDailyPlanAsync(string weather)
    {
        var prompt = $"Plan farming tasks for a {weather} day in Stardew Valley.";
        return await _planner.GetPlanAsync(prompt);
    }

    /// <summary>
    /// Enqueue a task into the scheduler.
    /// </summary>
    public void EnqueueTask(ITask task) => _scheduler.Enqueue(task);

    /// <summary>
    /// Enqueue a minimal set of tasks that cover the MVP flow of watering,
    /// harvesting, planting, inventory management and returning home.
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

    /// <summary>
    /// Runs queued tasks against the provided game state snapshot.
    /// </summary>
    public void Run(GameState state) => _scheduler.Execute(state);

    /// <summary>
    /// Called every update tick with the currently pressed keys. When the
    /// user presses O and P together the configuration menu is toggled.
    /// </summary>
    public void OnUpdate(InputState input)
    {
        if (input.IsPressed(Keys.O | Keys.P))
            _gui.Toggle();

        if (_gui.IsOpen)
            _gui.Draw();
    }
}
