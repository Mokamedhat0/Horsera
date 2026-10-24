namespace Horsera.Application.DTOs;

public sealed record RiderDto(int Id, string FullName, string? Email, string? PhoneNumber, string? ExperienceLevel);
public sealed record CreateRiderDto(string FullName, string? Email, string? PhoneNumber, string? ExperienceLevel);
public sealed record UpdateRiderDto(string FullName, string? Email, string? PhoneNumber, string? ExperienceLevel);
