public record SongDto
(
    int Id,
    string Name,
    string Band,
    string Creator,
    string Tuning,
    string SongStatus,
    TimeSpan Time,
    string? CoverImagePath
);