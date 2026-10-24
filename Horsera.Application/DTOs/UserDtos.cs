namespace Horsera.Application.DTOs;

public sealed record UserDto(string Id, string Email, string? DisplayName, string? PhoneNumber);
public sealed record UpdateUserProfileDto(string? DisplayName, string? PhoneNumber);
