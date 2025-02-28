using Application.Services;
using Domain.Aggregates;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Forms;

public sealed record FetchFormByIdQuery(Guid Id) : IRequest<UserForm?>;

public sealed class FetchFormByIdQueryHandler(IAppDbContext dbContext) : IRequestHandler<FetchFormByIdQuery, UserForm?>
{
    public async Task<UserForm?> Handle(FetchFormByIdQuery request, CancellationToken ct)
    {
        // caching will be possible here, and i will implement it if i have time,
        // just so i can show off, how much of a 10x developer i really am ;)

        return await dbContext.Forms.AsNoTracking()
            .Include(x => x.Fields)
            .FirstOrDefaultAsync(x => x.Id == request.Id, ct);
    }
}