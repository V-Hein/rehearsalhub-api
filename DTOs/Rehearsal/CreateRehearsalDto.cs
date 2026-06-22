public record CreateRehearsalDto
(
    string Name,
    int BandId,
    int SetlistId,
    int PlaceId,
    DateTime Date,

    int? TimeSeconds,
    string? Note
);