namespace Horsera.Application.DTOs;

public sealed record LessonDto(
    int Id, int HorseId, int InstructorId, DateTime StartAt, int DurationMinutes,
    int Capacity, decimal Price, string Status);

public sealed record CreateLessonDto(
    int HorseId, int InstructorId, DateTime StartAt, int DurationMinutes,
    int Capacity, decimal Price);

public sealed record UpdateLessonDto(
    int HorseId, int InstructorId, DateTime StartAt, int DurationMinutes,
    int Capacity, decimal Price);
