using Horsera.Application.DTOs;

namespace Horsera.Application.Abstractions.Services;

public interface ILessonService
{
    Task<IReadOnlyList<LessonDto>> GetUpcomingAsync(CancellationToken cancellationToken = default);
    Task<LessonDto?> GetByIdAsync(int lessonId, CancellationToken cancellationToken = default);
    Task<LessonDto> CreateAsync(CreateLessonDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int lessonId, UpdateLessonDto dto, CancellationToken cancellationToken = default);
    Task<bool> CancelAsync(int lessonId, CancellationToken cancellationToken = default);
}
