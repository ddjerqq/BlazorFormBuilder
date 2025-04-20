using Domain.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Models;
using Domain.Aggregates;
using Microsoft.AspNetCore.Http;
using Application.Services;

namespace Infrastructure.Service;

public sealed class JwtGenerator : IJwtGenerator
{
    public static readonly string ClaimsIssuer = "JWT__ISSUER".FromEnvRequired();
    public static readonly string ClaimsAudience = "JWT__AUDIENCE".FromEnvRequired();
    public static readonly string Key = "JWT__KEY".FromEnvRequired();
    public static readonly string KeyPath = "JWT__KEY_PATH".FromEnvRequired();
    public static readonly string Expiration = "JWT__EXPIRATION".FromEnvRequired();

    private readonly JwtSecurityTokenHandler _handler;
    private readonly SigningCredentials _signingCredentials;
    public readonly TokenValidationParameters TokenValidationParameters;

    public static readonly JwtBearerEvents Events = new()
    {
        OnMessageReceived = ctx =>
        {
            ctx.Request.Query.TryGetValue("access-token", out var query);
            ctx.Request.Headers.TryGetValue("access-token", out var header);
            ctx.Request.Cookies.TryGetValue("access-token", out var cookie);
            ctx.Token = (string?)query ?? (string?)header ?? cookie;
            return Task.CompletedTask;
        },
    };

    public JwtGenerator()
    {
        var rsa = RSA.Create();
        var keyBytes = File.ReadAllBytes(KeyPath);
        rsa.ImportFromEncryptedPem(Encoding.UTF8.GetString(keyBytes), Encoding.UTF8.GetBytes(Key));
        var securityKey = new RsaSecurityKey(rsa);

        _signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.RsaSha256Signature);
        _handler = new JwtSecurityTokenHandler();

        TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = ClaimsIssuer,
            ValidAudience = ClaimsAudience,
            IssuerSigningKey = securityKey,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidAlgorithms = [SecurityAlgorithms.RsaSha256Signature],
            ClockSkew = TimeSpan.Zero
        };
    }

    public string GenerateToken(IEnumerable<Claim> claims, TimeSpan? expiration = default!)
    {
        expiration ??= TimeSpan.FromSeconds(int.Parse(Expiration));

        var token = new JwtSecurityToken(
            ClaimsIssuer,
            ClaimsAudience,
            claims,
            expires: DateTime.UtcNow.Add(expiration.Value),
            signingCredentials: _signingCredentials);

        return _handler.WriteToken(token);
    }

    public string GenerateToken(User user, TimeSpan? expiration = null)
    {
        var claims = new List<Claim>
        {
            new ("sub", user.Id.ToString()),
            new ("preferred_username", user.Name),
            new ("email", user.Email)
        };
        return GenerateToken(claims, expiration);
    }

    public void SetTokensInsideCookie(TokensResponse tokens, HttpContext context)
    {
        context.Response.Cookies.Append("access-token", tokens.AccessToken,
            new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddSeconds(int.Parse(Expiration)),
                HttpOnly = true,
                IsEssential = true,
                Secure = true,
                SameSite = SameSiteMode.None
            });
        context.Response.Cookies.Append("refresh-token", tokens.RefreshToken,
            new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(7),
                HttpOnly = true,
                IsEssential = true,
                Secure = true,
                SameSite = SameSiteMode.None
            });
    }
}
