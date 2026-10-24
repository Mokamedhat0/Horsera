using Horsera.Domain.Common;
using Horsera.Domain.Entities.Staff;
using Horsera.Domain.Enums;

namespace Horsera.Domain.Entities.HorseAndCare;

public class HorseTreatment : BaseEntity
{
    public DateOnly StartDate { get; set; }
    public string? Instructions { get; set; }
    public TreatmentStatus Status { get; set; } = TreatmentStatus.Planned;
    public string? Notes { get; set; }


    public int HorseId { get; set; }
    public int VeterinarianId { get; set; }
 


    // Navigation properties
    public Horse Horse { get; set; } = null!;
    public Veterinarian Veterinarian { get; set; } = null!;
}
