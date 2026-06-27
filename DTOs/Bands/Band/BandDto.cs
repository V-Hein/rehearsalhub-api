public record BandDto
(
    int Id,
    string Name,
    List<int> GenreIds,
    List<string> Genres
);