namespace Horsera.Domain.Entities.User;

public class UserRole
{
    public int RoleId { get; set; }
    public Guid UserId { get; set; }
    public DateTime AssignedAt { get; set; }

    public Role Role { get; set; } = null!;
    public AppUser User { get; set; } = null!;
}
