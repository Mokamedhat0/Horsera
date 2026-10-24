using Horsera.Domain.Common;
using Horsera.Domain.Entities.Staff;
using Horsera.Domain.Enums;

namespace Horsera.Domain.Entities.HorseAndCare;

public class HorseMedicalReport : BaseEntity
{
    public DateOnly RecordedOn { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    public decimal? Height { get; set; }
    public decimal? Weight { get; set; }
    public HorseHealthStatus Status { get; set; } = HorseHealthStatus.Healthy;
    public DateOnly? NextDueDate { get; set; }
    public string? BatchNumber { get; set; }
    public string? Notes { get; set; }

    public int HorseId { get; set; }
    public int VeterinarianId { get; set; }

    public Horse Horse { get; set; } = null!;
    public Veterinarian Veterinarian { get; set; } = null!;
}
