using MediaBoomFileService.Presentation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProgramDependencies(builder.Configuration);

var app = builder.Build();

app.MapControllers();

app.Run();
