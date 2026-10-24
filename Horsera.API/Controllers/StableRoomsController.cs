using Horsera.Application.Abstractions.Services;
using Horsera.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Horsera.API.Controllers;

[ApiController]
[Route("api/stables/{stableId:int}/rooms")]
public sealed class StableRoomsController(IStableRoomService roomService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<StableRoomDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<StableRoomDto>>> GetByStable(
        int stableId,
        CancellationToken cancellationToken) =>
        Ok(await roomService.GetByStableAsync(stableId, cancellationToken));

    [HttpGet("{roomId:int}")]
    [ProducesResponseType<StableRoomDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StableRoomDto>> GetById(
        int stableId,
        int roomId,
        CancellationToken cancellationToken)
    {
        var room = await roomService.GetByIdAsync(roomId, cancellationToken);
        return room is null || room.StableId != stableId ? NotFound() : Ok(room);
    }

    [HttpPost]
    [ProducesResponseType<StableRoomDto>(StatusCodes.Status201Created)]
    public async Task<ActionResult<StableRoomDto>> Create(
        int stableId,
        CreateStableRoomDto request,
        CancellationToken cancellationToken)
    {
        if (request.StableId != stableId)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Stable ID mismatch",
                Detail = "The stable ID in the request body must match the route."
            });
        }

        var room = await roomService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(
            nameof(GetById),
            new { stableId, roomId = room.Id },
            room);
    }

    [HttpPut("{roomId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        int stableId,
        int roomId,
        UpdateStableRoomDto request,
        CancellationToken cancellationToken)
    {
        var room = await roomService.GetByIdAsync(roomId, cancellationToken);
        if (room is null || room.StableId != stableId)
        {
            return NotFound();
        }

        var updated = await roomService.UpdateAsync(roomId, request, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpPut("{roomId:int}/horses/{horseId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AssignHorse(
        int stableId,
        int roomId,
        int horseId,
        CancellationToken cancellationToken)
    {
        var room = await roomService.GetByIdAsync(roomId, cancellationToken);
        if (room is null || room.StableId != stableId)
        {
            return NotFound();
        }

        var assigned = await roomService.AssignHorseAsync(
            roomId,
            horseId,
            cancellationToken);
        return assigned ? NoContent() : NotFound();
    }

    [HttpDelete("{roomId:int}/horses/{horseId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveHorse(
        int stableId,
        int roomId,
        int horseId,
        CancellationToken cancellationToken)
    {
        var room = await roomService.GetByIdAsync(roomId, cancellationToken);
        if (room is null || room.StableId != stableId)
        {
            return NotFound();
        }

        var removed = await roomService.RemoveHorseAsync(
            roomId,
            horseId,
            cancellationToken);
        return removed ? NoContent() : NotFound();
    }
}
