public record RehearsalDto
(
    int Id,
    string Name,
    string Band,
    string Setlist,
    string Place,
    DateTime Date,

    TimeSpan? Time,
    string? Note
);