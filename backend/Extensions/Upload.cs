using backend.Records;
using Microsoft.AspNetCore.Builder;
using Npgsql;
using System.Diagnostics;

namespace backend.Extensions;



public static class UploadExtensions{

    public static Random random = new Random();

    public static string RandomFileId(int length){
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        return new string (Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public static WebApplication UploadRestApi(this WebApplication app){
        
        app.MapPost("/upload", async (Upload newUpload, NpgsqlConnection connection)=>{
            Console.WriteLine($"We received a new file {newUpload.fileName} to store !");

            await connection.OpenAsync();

            int check = -1;

            var maxUploadCheckQuery = "SELECT maxupload FROM Secret WHERE secret = @secret;";

            using (var cmd = new NpgsqlCommand(maxUploadCheckQuery, connection)){
                cmd.Parameters.AddWithValue("secret", newUpload.secret);

                using (var reader = await cmd.ExecuteReaderAsync()){
                    while(await reader.ReadAsync()){
                        check = reader.GetInt32(reader.GetOrdinal("maxupload"));
                    }
                }                
            }

            if (check < 1 ){
                return Results.Unauthorized();
            }

            var query1 = "INSERT INTO File (fileid, username, path, size, date, name, type) VALUES (@fileid, @username, @path, @size, @date, @name, @type);";
            var query2 = "INSERT INTO Upload (username, fileid, date, maxd, secret) VALUES (@username, @fileid, @date, @maxd, @secret);";
            var query3 = "UPDATE Secret SET maxupload = maxupload -1;";

            var query = $"{query1} {query2} {query3}";
            DateTime d = new DateTime(2024,01,01,0,0,0,DateTimeKind.Utc);
            using (var cmd = new NpgsqlCommand(query, connection)){
                cmd.Parameters.AddWithValue("fileid", RandomFileId(20));
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
