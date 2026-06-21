public record SongDto
(
    int Id,
    string Name,
    string Band,
    string Creator,
    string Tuning,
    TimeSpan Time,
    string? CoverImagePath
);