namespace Horsera.Application.DTOs;

public sealed record StableRoomDto(int Id, int StableId, string Name, int Capacity);
public sealed record CreateStableRoomDto(int StableId, string Name, int Capacity);
public sealed record UpdateStableRoomDto(string Name, int Capacity);
