public record BandMemberDto
(
    int Id,
    int BandId,
    int UserId,
    int RoleId,
    string Band,
    string User,
    string Role
);