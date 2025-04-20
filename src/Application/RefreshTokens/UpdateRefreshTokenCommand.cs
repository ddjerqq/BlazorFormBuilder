using Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.RefreshTokens
{
    public sealed record UpdateRefreshTokenCommand(Guid UserId,string oldToken) : IRequest<TokensResponse>;

    public sealed class UpdateRefreshTokenCommandValidator : AbstractValidator<UpdateRefreshTokenCommand>
    {
        public UpdateRefreshTokenCommandValidator()
        {
            RuleFor(t => t.oldToken).NotEmpty();
        }
    }

    internal sealed class UpdateRefreshTokenCommandHandler(
        IAppDbContext dbContext,
        IJwtGenerator jwtGenerator)
        : IRequestHandler<UpdateRefreshTokenCommand, TokensResponse>
    {
        public async Task<TokensResponse> Handle(UpdateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            
            var user = await dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            if (user is null)
            {
                throw new Exception("Invalid User");
            }

            var existingToken = await dbContext.RefreshTokens
                .FirstOrDefaultAsync(x =>
                        x.UserId == request.UserId &&
                        x.ExpiresAt > DateTime.UtcNow &&
                        x.Value == request.oldToken,
                    cancellationToken);

            if (existingToken is null)
            {
                throw new Exception("Refresh token is invalid or expired.");
            }
            await using var transaction = await dbContext.BeginTransactionAsync(cancellationToken);
            try
            {
                dbContext.RefreshTokens.Remove(existingToken);

                var newRefreshToken = RefreshToken.CreateNewToken(user.Id);
                await dbContext.RefreshTokens.AddAsync(newRefreshToken, cancellationToken);

                var newAccessToken = jwtGenerator.GenerateToken(user);

                await dbContext.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);
                return new TokensResponse(newAccessToken, newRefreshToken.Value);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }

}


