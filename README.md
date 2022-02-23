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
