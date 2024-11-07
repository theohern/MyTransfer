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

builder.Services.AddCors(options =>{
    options.AddPolicy("AllowSpecificOrigins",
    policy =>{
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowSpecificOrigins");

app.UploadRestApi();
app.SecretRestApi();

app.Run();
