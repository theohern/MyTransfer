using backend.Records;
using Microsoft.AspNetCore.Builder;
using Npgsql;
using System.Diagnostics;

namespace backend.Extensions;

public static class UploadExtensions{
    public static WebApplication UploadRestApi(this WebApplication app){
        
        app.MapPost("/upload", async (Upload newUpload, NpgsqlConnection connection)=>{
            Console.WriteLine($"We received a new file {newUpload.fileName} to store !");

            await connection.OpenAsync();

            var query = $"INSERT INTO File (fileid, username, path, size, date, name, type) VALUES (@fileid, @username, @path, @size, @date, @name, @type); INSERT INTO Upload (username, fileid, date, maxd, secret) VALUES (@username, @fileid, @date, @maxd, @secret)";
            DateTime d = new DateTime(2024,01,01,0,0,0,DateTimeKind.Utc);
            using (var cmd = new NpgsqlCommand(query, connection)){
                cmd.Parameters.AddWithValue("fileid", "aabbccdd");
                cmd.Parameters.AddWithValue("username", newUpload.username);
                cmd.Parameters.AddWithValue("path", "/tmp/lol");
                cmd.Parameters.AddWithValue("size", newUpload.size);
                cmd.Parameters.AddWithValue("date", d);
                cmd.Parameters.AddWithValue("name", newUpload.fileName);
                cmd.Parameters.AddWithValue("type", newUpload.type);
                cmd.Parameters.AddWithValue("maxd", newUpload.maxDownload);
                cmd.Parameters.AddWithValue("secret", newUpload.secret);

                await cmd.ExecuteNonQueryAsync();
            }

            return Results.Ok();
        });

        return app;
    } 

}
