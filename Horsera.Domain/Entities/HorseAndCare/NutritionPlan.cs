using Horsera.Domain.Common;
using Horsera.Domain.Entities.Staff;

namespace Horsera.Domain.Entities.HorseAndCare;

public class NutritionPlan : BaseEntity
{
    public string FeedingPlan { get; set; } = string.Empty;
    public TimeOnly FeedingTime { get; set; }
    public string Quantity { get; set; } = string.Empty;
    public decimal TotalCalories { get; set; }

    public int HorseId { get; set; }
    public int VeterinarianId { get; set; }
    public int StableEmployeeId { get; set; }


    // Navigation properties
    public StableEmployee StableEmployee { get; set; } = null!;
    public Horse Horse { get; set; } = null!;
    public Veterinarian Veterinarian { get; set; } = null!;
    
}
