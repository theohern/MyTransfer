namespace backend.Records;

public record class UploadRecord(
    string username, 
    int size, 
    int date,
    string type,
    string fileName, 
    string secret,
    int file, 
    int publickey
);