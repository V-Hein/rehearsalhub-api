public record SetlistDto
(
    int Id,
    string Name,
    string Band,
    int SongQuantity,
    int TotalTimeSeconds,
    List<SongListItemDto> Songs
);