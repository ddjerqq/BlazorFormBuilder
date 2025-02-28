using Application.Forms;
using Domain.Aggregates;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("/api/forms")]
public sealed class FormController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<UserForm>> Create([FromBody] UserForm form, CancellationToken ct)
    {
        var request = new CreateFormCommand(form);
        var result = await mediator.Send(request, ct);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserForm>>> GetAllForms([FromQuery] int page, [FromQuery] int perPage = 10, CancellationToken ct = default)
    {
        var request = new FetchAllFormsQuery(page, perPage);
        var result = await mediator.Send(request, ct);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var request = new FetchFormByIdQuery(id);
        var form = await mediator.Send(request);

        if (form is null)
            return NotFound();

        return Ok(form);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateForm(UserForm form)
    {
        var request = new UpdateFormCommand(form);
        await mediator.Send(request);
        return NoContent();
    }

    // delete
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteForm(Guid id)
    {
        var request = new DeleteFormCommand(id);
        await mediator.Send(request);
        return NoContent();
    }
}