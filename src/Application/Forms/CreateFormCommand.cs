using Application.Services;
using Domain.Aggregates;
using FluentValidation;
using MediatR;

namespace Application.Forms;

public sealed record CreateFormCommand(UserForm SuppliedForm,Guid UserId) : IRequest<UserForm>;

public sealed class CreateFormCommandValidator : AbstractValidator<CreateFormCommand>
{
    public CreateFormCommandValidator()
    {
        // more rules can be added if they are needed.
        RuleFor(x => x.SuppliedForm.Name).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
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

        request.SuppliedForm.UserId = request.UserId;
        dbContext.Forms.Add(request.SuppliedForm);
        await dbContext.SaveChangesAsync(ct);
        return request.SuppliedForm;
    }
}