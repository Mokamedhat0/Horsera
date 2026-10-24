using Horsera.Application.Abstractions.Services;
using Horsera.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Horsera.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class LessonsController(ILessonService lessonService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<LessonDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<LessonDto>>> GetUpcoming(
        CancellationToken cancellationToken) =>
        Ok(await lessonService.GetUpcomingAsync(cancellationToken));

    [HttpGet("{lessonId:int}")]
    [ProducesResponseType<LessonDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LessonDto>> GetById(
        int lessonId,
        CancellationToken cancellationToken)
    {
        var lesson = await lessonService.GetByIdAsync(lessonId, cancellationToken);
        return lesson is null ? NotFound() : Ok(lesson);
    }

    [HttpPost]
    [ProducesResponseType<LessonDto>(StatusCodes.Status201Created)]
    public async Task<ActionResult<LessonDto>> Create(
        CreateLessonDto request,
        CancellationToken cancellationToken)
    {
        var lesson = await lessonService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { lessonId = lesson.Id }, lesson);
    }

    [HttpPut("{lessonId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        int lessonId,
        UpdateLessonDto request,
        CancellationToken cancellationToken)
    {
        var updated = await lessonService.UpdateAsync(lessonId, request, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{lessonId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Cancel(
        int lessonId,
        CancellationToken cancellationToken)
    {
        var cancelled = await lessonService.CancelAsync(lessonId, cancellationToken);
        return cancelled ? NoContent() : NotFound();
    }
}
