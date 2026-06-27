public record CreateSongDto
(
    string Name,
    int BandId,
    int TuningId,
    int StatusId,
    int? TimeSeconds,
    string? CoverImagePath
);