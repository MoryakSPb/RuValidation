using System.Diagnostics.CodeAnalysis;

[assembly: ExcludeFromCodeCoverage]

WebApplicationBuilder builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.MapControllers();

await app.RunAsync();