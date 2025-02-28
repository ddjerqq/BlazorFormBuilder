using Application.Services;
using MediatR;

namespace Application.Forms;

public sealed record DeleteFormCommand(Guid Id) : IRequest;

public sealed class DeleteFormCommandHandler(IAppDbContext dbContext) : IRequestHandler<DeleteFormCommand>
{
    public async Task Handle(DeleteFormCommand request, CancellationToken ct)
    {
        var form = await dbContext.Forms.FindAsync([request.Id], ct);
        if (form is null)
            throw new InvalidOperationException("Form not found");

        dbContext.Forms.Remove(form);
        await dbContext.SaveChangesAsync(ct);
    }
}