using Horsera.Domain.Entities.User;
using Horsera.Domain.Entities.HorseAndCare;
using Horsera.Domain.Common;


namespace Horsera.Domain.Entities.Staff;

public class Veterinarian : BaseEntity
{
    public Guid UserId { get; set; }

    public string VeterinarianName { get; set; } = string.Empty;
    public string? ClinicName { get; set; }
    public string? Address { get; set; }
    public string? Specialization { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; } = true;


    // Navigation properties
    public AppUser User { get; set; } = null!;


    // Relationships
    public ICollection<Horse> Horses { get; set; } = new List<Horse>();
    public ICollection<HorseMedicalReport> MedicalReports { get; set; } = new List<HorseMedicalReport>();
    public ICollection<HorseVaccination> Vaccinations { get; set; } = new List<HorseVaccination>();
    public ICollection<HorseTreatment> Treatments { get; set; } = new List<HorseTreatment>();
    public ICollection<NutritionPlan> NutritionPlans { get; set; } = new List<NutritionPlan>();
}
