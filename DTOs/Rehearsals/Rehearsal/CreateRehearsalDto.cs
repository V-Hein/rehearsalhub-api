public record CreateRehearsalDto
(
    string Name,
    int BandId,
    int SetlistId,
    int PlaceId,
    int RehearsalStatusId,
    string Date,

    int? TimeSeconds,
    string? Note
);