namespace Horsera.Application.DTOs;

public sealed record StableDto(int Id, string Name, string? Address, string? Description);
public sealed record CreateStableDto(string Name, string? Address, string? Description);
public sealed record UpdateStableDto(string Name, string? Address, string? Description);
