using Horsera.Application.DTOs;

namespace Horsera.Application.Abstractions.Services;

public interface IHorseService
{
    Task<IReadOnlyList<HorseDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<HorseDto?> GetByIdAsync(int horseId, CancellationToken cancellationToken = default);
    Task<HorseDto> CreateAsync(CreateHorseDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int horseId, UpdateHorseDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int horseId, CancellationToken cancellationToken = default);
}
