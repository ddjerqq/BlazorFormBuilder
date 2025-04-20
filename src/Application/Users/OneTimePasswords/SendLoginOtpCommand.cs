using System.Security.Cryptography;
using Application.Services;
using FluentValidation;
using Infrastructure.Service;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Application.Users.OneTimePasswords
{
    public sealed record SendLoginOtpCommand(string Email) : IRequest<bool>;

    public sealed class SendLoginOtpCommandValidator : AbstractValidator<SendLoginOtpCommand>
    {
        public SendLoginOtpCommandValidator()
        {
            RuleFor(u => u.Email).NotEmpty().MinimumLength(2).MaximumLength(128).EmailAddress();
        }
    }

    public sealed class SendLoginOtpCommandHandler(IEmailSender emailService, IOneTimePasswordService otpService,ILogger<SendLoginOtpCommandHandler> logger)
        : IRequestHandler<SendLoginOtpCommand, bool>
    {
        public async Task<bool> Handle(SendLoginOtpCommand request, CancellationToken cancellationToken)
        {
            var code = otpService.CreateAndStoreOtp(request.Email);
            try
            {
                await emailService.SendEmailAsync(request.Email, subject: code, "Your One-Time Password",cancellationToken);
                return true;

            }
            catch (Exception ex)
            {
                logger.LogError($"{ex}");   
                return false;
            }

        }
    }
     
}
