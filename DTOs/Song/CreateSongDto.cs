public record CreateSongDto
(
    string Name,
    int BandId,
    int UserId,
    int TuningId,
    int SongStatusId,
    int? TimeSeconds,
    string? CoverImagePath
);