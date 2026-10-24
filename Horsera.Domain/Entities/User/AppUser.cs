using Horsera.Domain.Entities.Staff;
using Horsera.Domain.Entities.User.Notifications;



namespace Horsera.Domain.Entities.User;

public class AppUser
{
    public Guid UserId { get; set; }

    public string UserFirstName { get; set; } = string.Empty;
    public string UserSecondName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateOnly? DateOfBirth { get; set; }
    public string? Address { get; set; }
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }


    // Relationships
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();


    // Navigation properties
    public Rider? Rider { get; set; }
    public Instructor? Instructor { get; set; }
    public Receptionist? Receptionist { get; set; }
    public Veterinarian? Veterinarian { get; set; }
    public StableEmployee? StableEmployee { get; set; }
}
