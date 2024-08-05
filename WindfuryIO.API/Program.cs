using Microsoft.Extensions.Options;
using WarcraftLogs.Services;
using WindfuryIO.API.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<SecretConstants>(builder.Configuration.GetSection(nameof(SecretConstants))).AddOptions();
builder.Services.AddHttpClient<WarcraftLogsAuthorizationService>();
builder.Configuration.AddUserSecrets

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();