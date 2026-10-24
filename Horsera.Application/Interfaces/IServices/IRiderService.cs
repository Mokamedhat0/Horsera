using Horsera.Application.DTOs;

namespace Horsera.Application.Abstractions.Services;

public interface IRiderService
{
    Task<IReadOnlyList<RiderDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<RiderDto?> GetByIdAsync(int riderId, CancellationToken cancellationToken = default);
    Task<RiderDto> CreateAsync(CreateRiderDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int riderId, UpdateRiderDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int riderId, CancellationToken cancellationToken = default);
}
