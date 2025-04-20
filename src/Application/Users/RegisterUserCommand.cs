using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services;
using Domain.Aggregates;
using FluentValidation;
using MediatR;

namespace Application.Users
{

    public sealed record RegisterUserCommand(string Name, string Email) : IRequest<Guid>;

    public sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(u => u.Name).NotEmpty().MinimumLength(2).MaximumLength(128);
            RuleFor(u => u.Email).NotEmpty().MinimumLength(2).MaximumLength(128).EmailAddress();
        }
    }
    internal sealed class RegisterUserCommandHandler(IAppDbContext dbContext) : IRequestHandler<RegisterUserCommand, Guid>
    {
        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken ct)
        {
            var user = new User(request.Name, request.Email);
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync(ct);

            return user.Id;
        }
    }


}
