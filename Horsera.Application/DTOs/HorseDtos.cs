namespace Horsera.Application.DTOs;

public sealed record HorseDto(int Id, string Name, string? Breed, int? Age, string? Notes);
public sealed record CreateHorseDto(string Name, string? Breed, int? Age, string? Notes);
public sealed record UpdateHorseDto(string Name, string? Breed, int? Age, string? Notes);
