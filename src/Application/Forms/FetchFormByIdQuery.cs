using Application.Services;
using Domain.Aggregates;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Forms;

public sealed record FetchFormByIdQuery(Guid Id) : IRequest<UserForm?>;

public sealed class FetchFormByIdQueryHandler(IAppDbContext dbContext, IMemoryCache cache) : IRequestHandler<FetchFormByIdQuery, UserForm?>
{
    public async Task<UserForm?> Handle(FetchFormByIdQuery request, CancellationToken ct)
    {
        var form = await cache.GetOrCreateAsync<UserForm>($"form-{request.Id}", async entry =>
        {
            var form = await dbContext.Forms.AsNoTracking()
                .Include(x => x.Fields)
                .FirstOrDefaultAsync(x => x.Id == request.Id, ct);

            entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(1));
            return form!;
        });

        return form;
    }
}