
public record CreateUserDto
(
    string FirstName,
    string? LastName,
    string Email,
    string? Phone,
    string PasswordHash
);