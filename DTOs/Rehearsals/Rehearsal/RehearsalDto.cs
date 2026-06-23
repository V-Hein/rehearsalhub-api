using Microsoft.AspNetCore.SignalR.Protocol;

public record RehearsalDto
(
    int Id,
    string Name,
    string Band,
    string Setlist,
    string Place,
    string RehearsalStatus,
    DateTime Date,

    int? TimeSeconds,
    string? Note,

    List<RehearsalSongListItemDto> Songs
);