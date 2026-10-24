using Horsera.Application.DTOs;

namespace Horsera.Application.Abstractions.Services;

public interface ILessonBookingService
{
    Task<IReadOnlyList<LessonBookingDto>> GetByRiderAsync(int riderId, CancellationToken cancellationToken = default);
    Task<LessonBookingDto?> GetByIdAsync(int bookingId, CancellationToken cancellationToken = default);
    Task<LessonBookingDto> CreateAsync(CreateLessonBookingDto dto, CancellationToken cancellationToken = default);
    Task<bool> CancelAsync(int bookingId, int riderId, CancellationToken cancellationToken = default);
}
