using Horsera.Application.Abstractions.Services;
using Horsera.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Horsera.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class LessonBookingsController(ILessonBookingService bookingService) : ControllerBase
{
    [HttpGet("riders/{riderId:int}")]
    [ProducesResponseType<IReadOnlyList<LessonBookingDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<LessonBookingDto>>> GetByRider(
        int riderId,
        CancellationToken cancellationToken) =>
        Ok(await bookingService.GetByRiderAsync(riderId, cancellationToken));

    [HttpGet("{bookingId:int}")]
    [ProducesResponseType<LessonBookingDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LessonBookingDto>> GetById(
        int bookingId,
        CancellationToken cancellationToken)
    {
        var booking = await bookingService.GetByIdAsync(bookingId, cancellationToken);
        return booking is null ? NotFound() : Ok(booking);
    }

    [HttpPost]
    [ProducesResponseType<LessonBookingDto>(StatusCodes.Status201Created)]
    public async Task<ActionResult<LessonBookingDto>> Create(
        CreateLessonBookingDto request,
        CancellationToken cancellationToken)
    {
        var booking = await bookingService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { bookingId = booking.Id }, booking);
    }

    [HttpDelete("{bookingId:int}/riders/{riderId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Cancel(
        int bookingId,
        int riderId,
        CancellationToken cancellationToken)
    {
        var cancelled = await bookingService.CancelAsync(
            bookingId,
            riderId,
            cancellationToken);
        return cancelled ? NoContent() : NotFound();
    }
}
