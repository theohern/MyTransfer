using backend.Records;
using backend.Extensions;
using Npgsql;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");

builder.Services.AddScoped<NpgsqlConnection>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("PostgresConnection");
    return new NpgsqlConnection(connectionString);
});


// Afficher la chaîne de connexion dans la console
Console.WriteLine($"Connection String: {connectionString}");

var app = builder.Build();


app.MapPost("/upload", (UploadRecord newUpload)=>{
    Console.WriteLine($"get a new upload with username {newUpload.username} avec la size {newUpload.size}");
    return Results.Ok();
});

app.SecretRestApi();

app.Run();
