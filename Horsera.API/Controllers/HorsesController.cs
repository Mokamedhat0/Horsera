using Horsera.Application.Abstractions.Services;
using Horsera.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Horsera.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class HorsesController(IHorseService horseService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<HorseDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<HorseDto>>> GetAll(
        CancellationToken cancellationToken) =>
        Ok(await horseService.GetAllAsync(cancellationToken));

    [HttpGet("{horseId:int}")]
    [ProducesResponseType<HorseDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<HorseDto>> GetById(
        int horseId,
        CancellationToken cancellationToken)
    {
        var horse = await horseService.GetByIdAsync(horseId, cancellationToken);
        return horse is null ? NotFound() : Ok(horse);
    }

    [HttpPost]
    [ProducesResponseType<HorseDto>(StatusCodes.Status201Created)]
    public async Task<ActionResult<HorseDto>> Create(
        CreateHorseDto request,
        CancellationToken cancellationToken)
    {
        var horse = await horseService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { horseId = horse.Id }, horse);
    }

    [HttpPut("{horseId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        int horseId,
        UpdateHorseDto request,
        CancellationToken cancellationToken)
    {
        var updated = await horseService.UpdateAsync(horseId, request, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{horseId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        int horseId,
        CancellationToken cancellationToken)
    {
        var deleted = await horseService.DeleteAsync(horseId, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
