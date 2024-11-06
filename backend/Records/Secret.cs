namespace backend.Records;

public record class Secret(
    string secret,
    int maxSize,
    int maxUpload
);