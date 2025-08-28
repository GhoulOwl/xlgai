using System.Collections.Concurrent;

namespace AutoStardew.Tasks;

/// <summary>
/// Extremely small task scheduler that orders tasks by cost and executes
/// those that can run against the provided state snapshot.
/// </summary>
public class TaskQueue
{
    private readonly ConcurrentQueue<ITask> _tasks = new();

    public void Enqueue(ITask task) => _tasks.Enqueue(task);

    public void Execute(GameState state)
    {
        var ordered = _tasks.OrderBy(t => t.Cost(state)).ToList();
        foreach (var task in ordered)
        {
            if (task.CanExecute(state))
            {
                state = task.Apply(state);
                task.Execute();
            }
        }
    }
}
