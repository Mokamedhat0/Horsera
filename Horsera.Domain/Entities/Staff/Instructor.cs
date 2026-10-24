using Horsera.Domain.Common;
using Horsera.Domain.Entities.Lessons;
using Horsera.Domain.Entities.User;

namespace Horsera.Domain.Entities.Staff;

public class Instructor : BaseEntity
{
    public Guid UserId { get; set; }

    public decimal Salary { get; set; }
    public string? ImageUrl { get; set; }
    public int ExperienceYears { get; set; }
    public DateTime HireDate { get; set; }
    public string? Nickname { get; set; }


    // Navigation properties
    public AppUser User { get; set; } = null!;
    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}
