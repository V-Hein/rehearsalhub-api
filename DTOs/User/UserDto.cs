
public record UserDto
(
    int Id,
    string FirstName,
    string? LastName,
    string Email,
    string? Phone,
    List<Song> Songs
);