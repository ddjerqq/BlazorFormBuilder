using Application.Services;
using Domain.Aggregates;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Forms;

public sealed record UpdateFormCommand(UserForm Form) : IRequest;

public sealed class UpdateFormCommandHandler(IAppDbContext dbContext) : IRequestHandler<UpdateFormCommand>
{
    public async Task Handle(UpdateFormCommand request, CancellationToken ct)
    {
        var existingForm = await dbContext.Forms.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Form.Id, ct);
        if (existingForm is null)
            throw new InvalidOperationException("Form not found");

        request.Form.Modified = DateTime.UtcNow;
        dbContext.Entry(request.Form).State = EntityState.Modified;

        // if no request.Form fields contain the current existing field id, it has been deleted by the user.
        var deletedFields = existingForm.Fields.Where(existingField =>
                request.Form.Fields.All(x => x.Id != existingField.Id))
            .ToList();

        foreach (var existingField in deletedFields)
            dbContext.Entry(existingField).State = EntityState.Deleted;

        foreach (var (idx, field) in request.Form.Fields.Index())
        {
            field.Order = idx;
        }

        // skip deleted fields
        foreach (var field in request.Form.Fields.Where(field => deletedFields.All(x => x.Id != field.Id)))
        {
            // its either added, deleted, or modified
            dbContext.Entry(field).State = existingForm.Fields.Any(x => x.Id == field.Id)
                // if the field.id is present in existing.fields->id then its updated
                ? EntityState.Modified
                // if field.id is not present in existing.fields->id then its added
                : EntityState.Added;
        }

        await dbContext.SaveChangesAsync(ct);
    }
}