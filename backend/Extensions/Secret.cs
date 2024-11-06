using backend.Records;
using Microsoft.AspNetCore.Builder;
using Npgsql;
using System.Diagnostics;

namespace backend.Extensions;

public static class SecretExtensions{
    public static WebApplication SecretRestApi(this WebApplication app){


        app.MapGet("/secret", async (NpgsqlConnection connection) => {
            Console.WriteLine("Get all secrets");

            await connection.OpenAsync();

            var query = "select * from Secret;";

            var secrets = new List<Secret>();

            using (var cmd = new NpgsqlCommand(query, connection))
            using (var reader = await cmd.ExecuteReaderAsync()){
                while(await reader.ReadAsync()){
                    var secret = new Secret(
                        reader.GetString(reader.GetOrdinal("secret")),
                        reader.GetInt32(reader.GetOrdinal("MaxSize")),
                        reader.GetInt32(reader.GetOrdinal("MaxUpload"))
                    );
                    secrets.Add(secret);
                }
            }

            return Results.Ok(secrets);
        });

        app.MapGet("/secret/{secret}", async (string secret, NpgsqlConnection connection) => {
            Console.WriteLine($"Get secret {secret}");

            await connection.OpenAsync();

            var query = "SELECT * FROM Secret WHERE secret = @secret;";

            var secrets = new List<Secret>();

            using (var cmd = new NpgsqlCommand(query, connection)){
                cmd.Parameters.AddWithValue("secret", secret);
                using (var reader = await cmd.ExecuteReaderAsync()){
                    while(await reader.ReadAsync()){
                        var newSecret = new Secret(
                            reader.GetString(reader.GetOrdinal("secret")),
                            reader.GetInt32(reader.GetOrdinal("MaxSize")),
                            reader.GetInt32(reader.GetOrdinal("MaxUpload"))
                        );
                        secrets.Add(newSecret);
                    }
                }
            }

            if (secrets.Count == 0){
                return Results.NotFound($"No secret found for {secret}");
            }

            return Results.Ok(secrets);
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

        app.MapDelete("/secret/{secret}", async (string secret, NpgsqlConnection connection) =>{
            Console.WriteLine($"Delete secret {secret}");

            await connection.OpenAsync();

            var query = "DELETE FROM Secret WHERE secret = @secret";

            using (var cmd = new NpgsqlCommand(query, connection)){
                cmd.Parameters.AddWithValue("secret", secret);

                await cmd.ExecuteNonQueryAsync();
            }

            return Results.Ok();

        });

        app.MapPut("/secret", async (Secret newSecret, NpgsqlConnection connection)=>{
            Console.WriteLine($"update secret {newSecret.secret} with Maxsize {newSecret.maxSize} and MaxUpload {newSecret.maxUpload}");

            await connection.OpenAsync();

            var query = $"UPDATE Secret SET MaxSize = @MaxSize, MaxUpload = @MaxUpload WHERE secret = @secret";

            using (var cmd = new NpgsqlCommand(query, connection)){
                cmd.Parameters.AddWithValue("secret", newSecret.secret);
                cmd.Parameters.AddWithValue("MaxSize", newSecret.maxSize);
                cmd.Parameters.AddWithValue("MaxUpload", newSecret.maxUpload);

                var rowsAffected = await cmd.ExecuteNonQueryAsync();
                if (rowsAffected == 0){
                    return Results.NotFound($"No secret found for '{newSecret.secret}'");
                }

            }

            return Results.Ok();
        });

        return app;
    } 

}
