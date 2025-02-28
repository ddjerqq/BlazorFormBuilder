using Application.Services;
using Domain.Aggregates;
using FluentValidation;
using MediatR;

namespace Application.Forms;

public sealed record CreateFormCommand(UserForm SuppliedForm) : IRequest<UserForm>;

public sealed class CreateFormCommandValidator : AbstractValidator<CreateFormCommand>
{
    public CreateFormCommandValidator()
    {
        // more rules can be added if they are needed.
        RuleFor(x => x.SuppliedForm.Name).NotEmpty();
    }
}

public sealed class CreateFormCommandHandler(IAppDbContext dbContext) : IRequestHandler<CreateFormCommand, UserForm>
{
    public async Task<UserForm> Handle(CreateFormCommand request, CancellationToken ct)
    {
        foreach (var (idx, field) in request.SuppliedForm.Fields.Index())
        {
            field.Order = idx;
        }

        dbContext.Forms.Add(request.SuppliedForm);
        await dbContext.SaveChangesAsync(ct);
        return request.SuppliedForm;
    }
}