namespace Horsera.Application.DTOs;

public sealed record InstructorDto(int Id, string FullName, string? Email, string? PhoneNumber);
public sealed record CreateInstructorDto(string FullName, string? Email, string? PhoneNumber);
public sealed record UpdateInstructorDto(string FullName, string? Email, string? PhoneNumber);
