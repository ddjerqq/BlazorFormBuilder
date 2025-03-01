using Application.Services;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Forms;

public sealed record DeleteFormCommand(Guid Id) : IRequest;

public sealed class DeleteFormCommandHandler(IAppDbContext dbContext, IMemoryCache cache) : IRequestHandler<DeleteFormCommand>
{
    public async Task Handle(DeleteFormCommand request, CancellationToken ct)
    {
        var form = await dbContext.Forms.FindAsync([request.Id], ct);
        if (form is null)
            throw new InvalidOperationException("Form not found");

        cache.Remove($"form-{form.Id}");

        dbContext.Forms.Remove(form);
        await dbContext.SaveChangesAsync(ct);
    }
}