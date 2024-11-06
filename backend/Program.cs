using backend.Records;
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

app.MapPost("/secret", async (Secret newSecret, NpgsqlConnection connection)=>{
    Console.WriteLine($"get a new secret {newSecret.secret} with Maxsize {newSecret.maxSize} and MaxUpload {newSecret.maxUpload}");

    await connection.OpenAsync();

    var query = $"INSERT INTO secret (secret, MaxSize, MaxUpload) VALUES (@secret, @MaxSize, @MaxUpload)";

    using (var cmd = new NpgsqlCommand(query, connection)){
        cmd.Parameters.AddWithValue("secret", newSecret.secret);
        cmd.Parameters.AddWithValue("MaxSize", newSecret.maxSize);
        cmd.Parameters.AddWithValue("MaxUpload", newSecret.maxUpload);

        await cmd.ExecuteNonQueryAsync();
    }

    return Results.Ok();
});

app.Run();
