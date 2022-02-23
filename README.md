# FlaPo Backend
This is the repository for an interview assigned coding task.
At the end you will find two folders within this repository: 
- MinimalAPI
	- The MinimalAPI is an implementation based on the .NET 6 Minimal API approach. Very compact and dense without separate controller classes. For more information visit the following links:
	https://docs.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0
	https://docs.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-6.0&tabs=visual-studio
- ClassicAPI
	- This ClassicAPI is a more traditional approach using separate controller classes as one would expect usually. For more information visit the following links:
	https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-6.0
	https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-6.0&tabs=visual-studio

## Building
To build follow these steps: 
- Open up the FlaPo_Backend.sln solution file
- Restore the nuget packages
- Select the project you want to run (FlaPo_Backend_Minimal or FlaPo_Backend_Classic)
- A new Browser window should open, directing you to Swagger

## Observations about the Regex
With Commit [2c9fa29](https://github.com/Dakraid/FlaPo_Backend/commit/2c9fa29694e4d200d36b2e3d7f5fe898a640fdae) I implemented a few optimization oriented changes.
Using the Measure configuration, I noticed that the results were nearly identical between the previous implementation and the optimized one.

![Crude Measurement](https://github.com/Dakraid/FlaPo_Backend/blob/main/Assets/CrudeMeasurement.png)

When accounting for that one outlier (2860213) in the current set, the performance difference is under 2%. With the sample size as small and the environment not being controlled, I would chalk the difference up to average runtime deviances.

During my analysis, I read through the documentation, specifically https://docs.microsoft.com/en-us/dotnet/standard/base-types/best-practices, and learned more about my previous implementation using **static** Regex calls.

As it turns out, what I implemented with those performance oriented changes, is what static Regex calls do internally. While many factors play a role in the choice of type of Regex used, it seems that both static and instanced Regex implementations are equally valid and run roughly the same. More details are given within the linked Documentation.
