using Horsera.Application.Abstractions.Services;
using Horsera.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Horsera.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class StablesController(IStableService stableService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<StableDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<StableDto>>> GetAll(
        CancellationToken cancellationToken) =>
        Ok(await stableService.GetAllAsync(cancellationToken));

    [HttpGet("{stableId:int}")]
    [ProducesResponseType<StableDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StableDto>> GetById(
        int stableId,
        CancellationToken cancellationToken)
    {
        var stable = await stableService.GetByIdAsync(stableId, cancellationToken);
        return stable is null ? NotFound() : Ok(stable);
    }

    [HttpPost]
    [ProducesResponseType<StableDto>(StatusCodes.Status201Created)]
    public async Task<ActionResult<StableDto>> Create(
        CreateStableDto request,
        CancellationToken cancellationToken)
    {
        var stable = await stableService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { stableId = stable.Id }, stable);
    }

    [HttpPut("{stableId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        int stableId,
        UpdateStableDto request,
        CancellationToken cancellationToken)
    {
        var updated = await stableService.UpdateAsync(stableId, request, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{stableId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        int stableId,
        CancellationToken cancellationToken)
    {
        var deleted = await stableService.DeleteAsync(stableId, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
