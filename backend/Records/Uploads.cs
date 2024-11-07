namespace backend.Records;

public record class Upload(
    string username, 
    int size, 
    int date,
    string type,
    string fileName, 
    string secret,
    int file, 
    int publickey,
    int maxDownload
);