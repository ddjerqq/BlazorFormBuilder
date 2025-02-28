using Application.Services;
using Domain.Aggregates;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Forms;

public sealed record FetchAllFormsQuery(int Page, int PerPage) : IRequest<IEnumerable<UserForm>>;

public sealed class FetchAllFormsQueryHandler(IAppDbContext dbContext) : IRequestHandler<FetchAllFormsQuery, IEnumerable<UserForm>>
{
    public async Task<IEnumerable<UserForm>> Handle(FetchAllFormsQuery request, CancellationToken ct)
    {
        // caching will be possible here, and i will implement it if i have time,
        // just so i can show off, how much of a 10x developer i really am ;)

        var forms = await dbContext.Forms
            .OrderBy(x => x.Id)
            .Skip(request.Page * request.PerPage)
            .Take(request.PerPage)
            .ToListAsync(ct);

        return forms;
    }
}