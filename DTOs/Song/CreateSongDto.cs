public record CreateSongDto
(
    string Name,
    int BandId,
    int UserId,
    int TuningId,
    int? TimeSeconds,
    string? CoverImagePath
);