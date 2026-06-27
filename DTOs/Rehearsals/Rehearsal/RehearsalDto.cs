using Microsoft.AspNetCore.SignalR.Protocol;

public record RehearsalDto
(
    int Id,
    string Name,
    int BandId,
    string Band,
    int SetlistId,
    string Setlist,
    int PlaceId,
    string Place,
    int StatusId,
    string Status,
    DateTime Date,

    int? TimeSeconds,
    string? Note,

    List<RehearsalSongListItemDto> Songs
);