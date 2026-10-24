using Horsera.Application.DTOs;

namespace Horsera.Application.Abstractions.Services;



public interface IInstructorService
{
    Task<IReadOnlyList<InstructorDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<InstructorDto?> GetByIdAsync(int instructorId, CancellationToken cancellationToken = default);
    Task<InstructorDto> CreateAsync(CreateInstructorDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int instructorId, UpdateInstructorDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int instructorId, CancellationToken cancellationToken = default);
}
