using Application.Services;
using FluentValidation;
using MediatR;

namespace Application.Users.OneTimePasswords
{ 
    public sealed record ValidateLoginOtpCommand(string Email,string Code) : IRequest<bool>;

    public sealed class ValidateLoginOtpCommandValidator : AbstractValidator<ValidateLoginOtpCommand>
    {
        public ValidateLoginOtpCommandValidator()
        {
            RuleFor(u => u.Email).NotEmpty().MinimumLength(2).MaximumLength(128).EmailAddress();
            RuleFor(e => e.Code).NotEmpty().Must(x => int.TryParse(x, out var val) && val >= 0)
                .WithMessage("Invalid Code.");
        }
    }
    public sealed class ValidateLoginOtpCommandHandler(IOneTimePasswordService otpService) : IRequestHandler<ValidateLoginOtpCommand, bool>
    {
        public Task<bool> Handle(ValidateLoginOtpCommand request, CancellationToken cancellationToken)
        {
         return Task.FromResult(otpService.ValidateOtp(email: request.Email, requestCode: request.Code));
        }
    }

}
