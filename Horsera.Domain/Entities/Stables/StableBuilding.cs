using Horsera.Domain.Common;
using Horsera.Domain.Common.Exceptions;

namespace Horsera.Domain.Entities.Stables;

public class StableBuilding : BaseEntity
{
    private StableBuilding()
    {
    }

    public StableBuilding(string name)
    {
        Rename(name);
    }

    public string Name { get; private set; } = string.Empty;
    public string? Address { get; set; }
    public string? Description { get; set; }

    public ICollection<StableRoom> Rooms { get; set; } = new List<StableRoom>();

    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainValidationException("Stable name is required.");
        }

        Name = name.Trim();
    }
}
