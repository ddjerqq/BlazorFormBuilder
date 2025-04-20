using System.Security.Cryptography;
using Domain.Aggregates;

namespace Application.Models;

public sealed class RefreshToken
{

    public Guid TokenId { get; private set; }
    public Guid UserId { get; private set; }
    public string Value { get; private set; }
    public DateTime ExpiresAt { get; private set; }


    private RefreshToken() { }

    public static RefreshToken CreateNewToken(Guid userId)
    {
        return new()
        {
            TokenId = Guid.NewGuid(),
            UserId = userId,
            ExpiresAt = DateTime.Now.AddDays(7),
            Value = GenerateNewValue()
        };
    }

    private static string GenerateNewValue()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    }

}