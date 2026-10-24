using Horsera.Application.Abstractions.Services;
using Horsera.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Horsera.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class RidersController(IRiderService riderService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<RiderDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<RiderDto>>> GetAll(
        CancellationToken cancellationToken) =>
        Ok(await riderService.GetAllAsync(cancellationToken));

    [HttpGet("{riderId:int}")]
    [ProducesResponseType<RiderDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RiderDto>> GetById(
        int riderId,
        CancellationToken cancellationToken)
    {
        var rider = await riderService.GetByIdAsync(riderId, cancellationToken);
        return rider is null ? NotFound() : Ok(rider);
    }

    [HttpPost]
    [ProducesResponseType<RiderDto>(StatusCodes.Status201Created)]
    public async Task<ActionResult<RiderDto>> Create(
        CreateRiderDto request,
        CancellationToken cancellationToken)
    {
        var rider = await riderService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { riderId = rider.Id }, rider);
    }

    [HttpPut("{riderId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        int riderId,
        UpdateRiderDto request,
        CancellationToken cancellationToken)
    {
        var updated = await riderService.UpdateAsync(riderId, request, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{riderId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        int riderId,
        CancellationToken cancellationToken)
    {
        var deleted = await riderService.DeleteAsync(riderId, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
