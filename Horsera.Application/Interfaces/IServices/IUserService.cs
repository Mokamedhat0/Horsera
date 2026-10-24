using Horsera.Application.DTOs;

namespace Horsera.Application.Abstractions.Services;

public interface IUserService
{
    Task<UserDto?> GetByIdAsync(string userId, CancellationToken cancellationToken = default);
    Task<UserDto?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<UserDto> UpdateProfileAsync(string userId, UpdateUserProfileDto dto, CancellationToken cancellationToken = default);
}
