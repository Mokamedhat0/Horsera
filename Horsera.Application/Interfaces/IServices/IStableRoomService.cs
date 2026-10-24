using Horsera.Application.DTOs;

namespace Horsera.Application.Abstractions.Services;

public interface IStableRoomService
{
    Task<IReadOnlyList<StableRoomDto>> GetByStableAsync(int stableId, CancellationToken cancellationToken = default);
    Task<StableRoomDto?> GetByIdAsync(int roomId, CancellationToken cancellationToken = default);
    Task<StableRoomDto> CreateAsync(CreateStableRoomDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int roomId, UpdateStableRoomDto dto, CancellationToken cancellationToken = default);
    Task<bool> AssignHorseAsync(int roomId, int horseId, CancellationToken cancellationToken = default);
    Task<bool> RemoveHorseAsync(int roomId, int horseId, CancellationToken cancellationToken = default);
}
