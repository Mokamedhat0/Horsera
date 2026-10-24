using Horsera.Application.Abstractions.Services;
using Horsera.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Horsera.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class InstructorsController(IInstructorService instructorService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<InstructorDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<InstructorDto>>> GetAll(
        CancellationToken cancellationToken) =>
        Ok(await instructorService.GetAllAsync(cancellationToken));

    [HttpGet("{instructorId:int}")]
    [ProducesResponseType<InstructorDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InstructorDto>> GetById(
        int instructorId,
        CancellationToken cancellationToken)
    {
        var instructor = await instructorService.GetByIdAsync(instructorId, cancellationToken);
        return instructor is null ? NotFound() : Ok(instructor);
    }

    [HttpPost]
    [ProducesResponseType<InstructorDto>(StatusCodes.Status201Created)]
    public async Task<ActionResult<InstructorDto>> Create(
        CreateInstructorDto request,
        CancellationToken cancellationToken)
    {
        var instructor = await instructorService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(
            nameof(GetById),
            new { instructorId = instructor.Id },
            instructor);
    }

    [HttpPut("{instructorId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        int instructorId,
        UpdateInstructorDto request,
        CancellationToken cancellationToken)
    {
        var updated = await instructorService.UpdateAsync(
            instructorId,
            request,
            cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{instructorId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        int instructorId,
        CancellationToken cancellationToken)
    {
        var deleted = await instructorService.DeleteAsync(instructorId, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
