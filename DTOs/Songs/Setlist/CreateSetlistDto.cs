public record CreateSetlistDto
(
    string Name,
    int BandId,
    int[] SongIds
);