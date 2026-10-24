using Horsera.Domain.Common;
using Horsera.Domain.Entities.Lessons;
using Horsera.Domain.Entities.User;

namespace Horsera.Domain.Entities.Staff;

public class Rider : BaseEntity
{
    public Guid UserId { get; set; }

    public string FullName { get; set; } = string.Empty;
    public string? ExperienceLevel { get; set; }
    public string? ImageUrl { get; set; }
    public string? MedicalNotes { get; set; }
    public DateTime RegistrationDate { get; set; }
    public bool IsActive { get; set; } = true;


    // Navigation properties
    public AppUser User { get; set; } = null!;


    // Relationships
    public ICollection<LessonBooking> LessonBookings { get; set; } = new List<LessonBooking>();
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
