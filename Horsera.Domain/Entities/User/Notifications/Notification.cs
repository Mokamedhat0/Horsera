namespace Horsera.Domain.Entities.User.Notifications;

public class Notification
{
    public int NotificationId { get; set; }
    public Guid UserId { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string NotificationType { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? ReadAt { get; set; }

    // Navigation properties
    public AppUser User { get; set; } = null!;
}
