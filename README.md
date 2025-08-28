# AutoStardew

A proof-of-concept Stardew Valley automation mod powered by a local OpenAI-compatible API.

This repository contains a minimal C# class library demonstrating the core architecture and a tiny
console demo that runs the MVP task loop:

- **AI Planner**: simple wrapper around any OpenAI-compatible chat completion endpoint.
- **Blackboard**: shared key/value store for game state snapshots.
- **Task system**: pluggable tasks implementing `ITask` and scheduled by `TaskQueue`.
- **MVP automation**: watering, harvesting, planting, chest storage and returning home based on a
  simplified `GameState` snapshot.

The demo doesn't interact with the actual game yet, but it exercises the automation flow and prints
log messages to the console.

## Building

### Library

```bash
dotnet build src/AutoStardew/AutoStardew.csproj
```

### Demo

```bash
dotnet run --project src/AutoStardew.Demo/AutoStardew.Demo.csproj
```

Ensure `OPENAI_API_KEY` and `OPENAI_API_BASE` are set if you want to exercise the `AiPlanner` against a local or remote endpoint.
