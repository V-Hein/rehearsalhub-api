public record CreateSongDto
(
    string Name,
    int BandId,
    int TuningId,
    int SongStatusId,
    int? TimeSeconds,
    string? CoverImagePath
);