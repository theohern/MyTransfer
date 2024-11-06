using backend.Records;
using Microsoft.AspNetCore.Builder;
using Npgsql;
using System.Diagnostics;

namespace backend.Extensions;

public static class UploadExtensions{
    public static WebApplication UploadRestApi(this WebApplication app){
        
        app.MapPost("/upload", (UploadRecord newUpload)=>{
            Console.WriteLine($"get a new upload with username {newUpload.username} avec la size {newUpload.size}");
            return Results.Ok();
        });

        return app;
    } 

}
