public record CreateSongDto
(
    string Name,
    string Band,
    int UserId,
    int TuningId,
    int? TimeSeconds,
    string? CoverImagePath
);