public record SetlistDto
(
    int Id,
    string Name,
    int BandId,
    string Band,
    List<int> SongIds,
    int SongQuantity,
    int TotalTimeSeconds,
    List<SongListItemDto> Songs
);