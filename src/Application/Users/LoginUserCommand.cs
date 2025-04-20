using Application.Services;
using Domain.Aggregates;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users
{
    public sealed record LoginUserCommand(string Name, string Email) : IRequest<User?>;

    public sealed class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(u => u.Name).NotEmpty().MinimumLength(2).MaximumLength(128);
            RuleFor(u => u.Email).NotEmpty().MinimumLength(2).MaximumLength(128).EmailAddress();
        }
    }

    internal sealed class LoginUserCommandHandler(IAppDbContext dbContext)
        : IRequestHandler<LoginUserCommand, User?>
    {
        public async Task<User?> Handle(LoginUserCommand request, CancellationToken ct)
        {
            var email = request.Email.ToLowerInvariant();
            var name = request.Name.ToLowerInvariant();
                    
            var user = await dbContext.Users
                .FirstOrDefaultAsync(u =>
                        u.Email.ToLower() == email &&
                        u.Name.ToLower() == name,
                    ct);

            if (user is null || !user.Name.Equals(request.Name, StringComparison.CurrentCultureIgnoreCase))
                return null;
            return user;
        }
    }

}
