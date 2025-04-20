using Application.Models;
using Application.Services;
using Domain.Aggregates;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.RefreshTokens;

public sealed record CreateRefreshTokenCommand(Guid UserId) : IRequest<TokensResponse>;

public sealed class CreateRefreshTokenCommandHandler(
    IAppDbContext dbContext,
    IJwtGenerator jwtGenerator)
    : IRequestHandler<CreateRefreshTokenCommand, TokensResponse>
{
    public async Task<TokensResponse> Handle(CreateRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        if (user is null)
        {
            throw new Exception("Invalid User");
        }

        var existingToken = await dbContext.RefreshTokens
            .FirstOrDefaultAsync(x => x.UserId == request.UserId , cancellationToken);

        
        await using var transaction = await dbContext.BeginTransactionAsync(cancellationToken);

        try
        {
            if (existingToken is not null)
            {
                dbContext.RefreshTokens.Remove(existingToken);
            }
            var accessToken = jwtGenerator.GenerateToken(user);
            var refreshToken = RefreshToken.CreateNewToken(user.Id);

            await dbContext.RefreshTokens.AddAsync(refreshToken, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return new TokensResponse(accessToken,refreshToken.Value);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
