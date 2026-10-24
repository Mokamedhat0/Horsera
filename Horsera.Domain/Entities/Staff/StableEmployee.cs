using Horsera.Domain.Common;
using Horsera.Domain.Entities.HorseAndCare;
using Horsera.Domain.Entities.User;

namespace Horsera.Domain.Entities.Staff;

public class StableEmployee : BaseEntity
{
    public Guid UserId { get; set; }

    public string FullName { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string? ImageUrl { get; set; }
    public string? Notes { get; set; }


    // Navigation properties
    public AppUser User { get; set; } = null!;
    public ICollection<Horse> Horses { get; set; } = new List<Horse>();
    public ICollection<NutritionPlan> NutritionPlans { get; set; } = new List<NutritionPlan>();
}
