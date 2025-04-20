using Domain.Aggregates;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Models;

namespace Application.Services
{
    public interface IJwtGenerator
    {
        public string GenerateToken(IEnumerable<Claim> claims, TimeSpan? expiration = null);
        public string GenerateToken(User user, TimeSpan? expiration = null);
        public void SetTokensInsideCookie(TokensResponse tokens, HttpContext context);
    }
}
