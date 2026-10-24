using Horsera.Domain.Common;
using Horsera.Domain.Common.Exceptions;
using Horsera.Domain.Entities.HorseAndCare;

namespace Horsera.Domain.Entities.Stables;

public class StableRoom : BaseEntity
{
    private readonly List<Horse> _horses = [];

    private StableRoom()
    {
    }

    public StableRoom(int stableBuildingId, string name, int capacity)
    {
        if (stableBuildingId <= 0)
        {
            throw new DomainValidationException("A valid stable building is required.");
        }

        StableBuildingId = stableBuildingId;
        Rename(name);
        ChangeCapacity(capacity);
    }

    public string Name { get; private set; } = string.Empty;
    public int Capacity { get; private set; }
    public string? Note { get; set; }
    public bool IsActive { get; private set; } = true;


    public int StableBuildingId { get; private set; }


    // Navigation properties
    public StableBuilding StableBuilding { get; private set; } = null!;
    public IReadOnlyCollection<Horse> Horses => _horses;

    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainValidationException("Stable room name is required.");
        }

        Name = name.Trim();
    }

    public void ChangeCapacity(int capacity)
    {
        if (capacity <= 0)
        {
            throw new DomainValidationException("Stable room capacity must be greater than zero.");
        }

        if (capacity < Horses.Count)
        {
            throw new BusinessRuleException("Capacity cannot be lower than the number of horses in the room.");
        }

        Capacity = capacity;
    }

    public void AssignHorse(Horse horse)
    {
        ArgumentNullException.ThrowIfNull(horse);

        if (!IsActive)
        {
            throw new BusinessRuleException("Horses cannot be assigned to an inactive room.");
        }

        if (horse.StableRoomId is not null && horse.StableRoomId != Id)
        {
            throw new BusinessRuleException("The horse is already assigned to another room.");
        }

        if (_horses.Any(existingHorse =>
                ReferenceEquals(existingHorse, horse) ||
                (horse.Id != 0 && existingHorse.Id == horse.Id)))
        {
            return;
        }

        if (Horses.Count >= Capacity)
        {
            throw new BusinessRuleException("The stable room has no available capacity.");
        }

        horse.AssignToRoom(this);
        _horses.Add(horse);
    }

    public void RemoveHorse(Horse horse)
    {
        ArgumentNullException.ThrowIfNull(horse);

        if (!_horses.Remove(horse))
        {
            return;
        }

        horse.RemoveFromRoom();
    }

    public void Deactivate() => IsActive = false;

    public void Activate() => IsActive = true;
}
