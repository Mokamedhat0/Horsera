using Horsera.Domain.Common;
using Horsera.Domain.Entities.Staff;

namespace Horsera.Domain.Entities.HorseAndCare;

public class HorseVaccination : BaseEntity
{
    public string VaccinationName { get; set; } = string.Empty;
    public string? VaccinationDescription { get; set; }
    public DateOnly VaccinationDay { get; set; }
    public bool IsActive { get; set; } = true;

    public int VeterinarianId { get; set; }
    public int HorseId { get; set; }

    public Veterinarian Veterinarian { get; set; } = null!;
    public Horse Horse { get; set; } = null!;
}
