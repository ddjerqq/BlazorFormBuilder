using Application.Forms;
using Domain.Aggregates;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers;

[ApiController]
[Route("/api/forms")]
public sealed class FormController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<UserForm>> Create([FromBody] UserForm form, CancellationToken ct)
    {
        if(!TryGetUserId(out Guid userId))
            return Unauthorized("Invalid user context");

        var request = new CreateFormCommand(form, userId);
        var result = await mediator.Send(request, ct);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserForm>>> GetAllForms([FromQuery] int page, [FromQuery] int perPage = 10, CancellationToken ct = default)
    {
        if (!TryGetUserId(out Guid userId))
            return Unauthorized("Invalid user context");

        var request = new FetchAllFormsQuery(page, perPage);
        var result = await mediator.Send(request, ct);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        if (!TryGetUserId(out Guid userId))
            return Unauthorized("Invalid user context");

        var request = new FetchFormByIdQuery(id);
        var form = await mediator.Send(request);

        if (form is null)
            return NotFound();

        return Ok(form);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateForm(UserForm form)
    {
        if (!TryGetUserId(out Guid userId))
            return Unauthorized("Invalid user context");

        var request = new UpdateFormCommand(form);
        await mediator.Send(request);
        return NoContent();
    }

    // delete
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteForm(Guid id)
    {
        if (!TryGetUserId(out Guid userId))
            return Unauthorized("Invalid user context");
        var request = new DeleteFormCommand(id);
        await mediator.Send(request);
        return NoContent();
    }

    private bool TryGetUserId(out Guid userId)
    {
        userId = default;

        if (!User.Identity?.IsAuthenticated ?? true)
            return false;

        var userIdStr = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdStr) || !Guid.TryParse(userIdStr, out userId))
            return false;

        return true;
    }

}