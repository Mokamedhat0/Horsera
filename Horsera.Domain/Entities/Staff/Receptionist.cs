using Horsera.Domain.Common;
using Horsera.Domain.Entities.User;

namespace Horsera.Domain.Entities.Staff;

public class Receptionist : BaseEntity
{
    public Guid UserId { get; set; }
    public string? PhoneNumber { get; set; }


    // Navigation properties
    public AppUser User { get; set; } = null!;
}
