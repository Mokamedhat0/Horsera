using Horsera.Application.DTOs;

namespace Horsera.Application.Abstractions.Services;

public interface IStableService
{
    Task<IReadOnlyList<StableDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<StableDto?> GetByIdAsync(int stableId, CancellationToken cancellationToken = default);
    Task<StableDto> CreateAsync(CreateStableDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int stableId, UpdateStableDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int stableId, CancellationToken cancellationToken = default);
}
