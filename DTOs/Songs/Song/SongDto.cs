public record SongDto
(
    int Id,
    string Name,
    int BandId,
    string Band,
    string Creator,
    int TuningId,
    string Tuning,
    int StatusId,
    string Status,
    int? TimeSeconds,
    string? CoverImagePath
);