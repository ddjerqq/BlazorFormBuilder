using Application.Services;
using Domain.Aggregates;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Forms;

public sealed record FetchAllFormsQuery(int Page, int PerPage) : IRequest<IEnumerable<UserForm>>;

public sealed class FetchAllFormsQueryHandler(IAppDbContext dbContext, IMemoryCache cache) : IRequestHandler<FetchAllFormsQuery, IEnumerable<UserForm>>
{
    public async Task<IEnumerable<UserForm>> Handle(FetchAllFormsQuery request, CancellationToken ct)
    {
        var forms = await cache.GetOrCreateAsync<List<UserForm>>($"all-forms-{request.Page}-{request.PerPage}", async entry =>
        {
            var forms = await dbContext.Forms
                .OrderByDescending(x => x.Modified)
                .Skip(request.Page * request.PerPage)
                .Take(request.PerPage)
                .ToListAsync(ct);

            entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(1));
            return forms;
        });


        return forms!;
    }
}