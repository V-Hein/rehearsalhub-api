public record CreateRehearsalDto
(
    string Name,
    int BandId,
    int SetlistId,
    int PlaceId,
    int RehearsalStatusId,
    DateTime Date,

    int? TimeSeconds,
    string? Note
);