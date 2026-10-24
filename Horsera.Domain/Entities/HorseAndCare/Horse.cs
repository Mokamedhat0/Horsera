using Horsera.Domain.Common;
using Horsera.Domain.Common.Exceptions;
using Horsera.Domain.Entities.Lessons;
using Horsera.Domain.Entities.Stables;
using Horsera.Domain.Entities.Staff;
using Horsera.Domain.Enums;

namespace Horsera.Domain.Entities.HorseAndCare;

public class Horse : BaseEntity
{
    private Horse()
    {
    }

    public Horse(string name, DateOnly? dateOfBirth = null)
    {
        Rename(name);
        UpdateDateOfBirth(dateOfBirth);
        CreatedAt = DateTime.UtcNow;
    }

    // Properties
    public string Name { get; private set; } = string.Empty;
    public string? Breed { get; set; }
    public string? Gender { get; set; }
    public string? Color { get; set; }
    public DateOnly? DateOfBirth { get; private set; }
    public int? Age => DateOfBirth is null
        ? null
        : CalculateAge(DateOfBirth.Value, DateOnly.FromDateTime(DateTime.UtcNow));
    public string? Notes { get; set; }
    public HorseStatus Status { get; private set; } = HorseStatus.Available;
    public string? PhotoUrl { get; set; }

    // Foreign keys
    public int? StableRoomId { get; private set; }
    public int? StableEmployeeId { get; private set; }
    public int? VeterinarianId { get; private set; }


    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }


    public StableRoom? StableRoom { get; private set; }
    public StableEmployee? StableEmployee { get; private set; }
    public Veterinarian? Veterinarian { get; private set; }


    //relationships
    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
    public ICollection<HorseMedicalReport> MedicalReports { get; set; } = new List<HorseMedicalReport>();
    public ICollection<HorseVaccination> Vaccinations { get; set; } = new List<HorseVaccination>();
    public ICollection<HorseTreatment> Treatments { get; set; } = new List<HorseTreatment>();
    public ICollection<NutritionPlan> NutritionPlans { get; set; } = new List<NutritionPlan>();

    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainValidationException("Horse name is required.");
        }

        Name = name.Trim();
    }

    public void UpdateDateOfBirth(DateOnly? dateOfBirth)
    {
        if (dateOfBirth is { } value && value > DateOnly.FromDateTime(DateTime.UtcNow))
        {
            throw new DomainValidationException("Date of birth cannot be in the future.");
        }

        DateOfBirth = dateOfBirth;
    }

    public void ChangeStatus(HorseStatus status)
    {
        if (!Enum.IsDefined(status))
        {
            throw new DomainValidationException("Horse status is invalid.");
        }

        if (Status == HorseStatus.Retired && status != HorseStatus.Retired)
        {
            throw new BusinessRuleException("A retired horse cannot return to active service.");
        }

        Status = status;
    }

    public void AssignStableEmployee(int? stableEmployeeId)
    {
        if (stableEmployeeId is <= 0)
        {
            throw new DomainValidationException("Stable employee ID must be positive.");
        }

        StableEmployeeId = stableEmployeeId;
    }

    public void AssignVeterinarian(int? veterinarianId)
    {
        if (veterinarianId is <= 0)
        {
            throw new DomainValidationException("Veterinarian ID must be positive.");
        }

        VeterinarianId = veterinarianId;
    }

    internal void AssignToRoom(StableRoom room)
    {
        StableRoom = room;
        StableRoomId = room.Id == 0 ? null : room.Id;
    }

    internal void RemoveFromRoom()
    {
        StableRoom = null;
        StableRoomId = null;
    }

    private static int CalculateAge(DateOnly dateOfBirth, DateOnly today)
    {
        var age = today.Year - dateOfBirth.Year;
        if (dateOfBirth > today.AddYears(-age))
        {
            age--;
        }

        return age;
    }
}
