public record CreateRehearsalDto
(
    string Name,
    int BandId,
    int SetlistId,
    int PlaceId,
    int StatusId,
    string Date,

    int? TimeSeconds,
    string? Note
);