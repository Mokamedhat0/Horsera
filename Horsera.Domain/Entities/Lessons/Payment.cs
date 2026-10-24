using Horsera.Domain.Common;
using Horsera.Domain.Entities.Staff;
using Horsera.Domain.Enums;

namespace Horsera.Domain.Entities.Lessons;

public class Payment : BaseEntity
{
    public int RiderId { get; set; }
    public int? BookingId { get; set; }
    public int? ReceptionistId { get; set; }


    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Other;
    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
    public string? TransactionReference { get; set; }
    public string? Notes { get; set; }

    // Navigation properties
    public Rider Rider { get; set; } = null!;
    public LessonBooking? Booking { get; set; }
    public Receptionist? Receptionist { get; set; }
}
