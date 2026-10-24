namespace Horsera.Application.DTOs;

public sealed record LessonBookingDto(
    int Id, int LessonId, int RiderId, DateTime BookedAt, string Status);

public sealed record CreateLessonBookingDto(int LessonId, int RiderId);
