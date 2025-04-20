using System.Security.Claims;
using System.Xml.Linq;
using Application.RefreshTokens;
using Application.Services;
using Application.Users;
using Domain.Aggregates;
using Infrastructure.Service;          
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;


[ApiController]
[Route("api/auth")]
public sealed class AuthController(IMediator mediator,IJwtGenerator jwtGenerator) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult> RegisterAsync([FromBody] RegisterRequest request, CancellationToken ct)
    {
        var command = new RegisterUserCommand(request.Name, request.Email); 
        var userId = await mediator.Send(command, ct);


        var refcommand = new CreateRefreshTokenCommand(userId);
        var tokensResponse = await mediator.Send(refcommand, ct);
        jwtGenerator.SetTokensInsideCookie(tokensResponse, HttpContext);

        return Ok(new { UserId = userId });

    }   

    [HttpPost("login")]
        public async Task<ActionResult> AuthenticateAsync([FromBody] LoginRequest request, CancellationToken ct)
        {
            var command = new LoginUserCommand(request.Name, request.Email);
            var user = await mediator.Send(command, ct);

            if (user == null)
            {
                return Unauthorized("Invalid credentials.");
            }


            var refcommand = new CreateRefreshTokenCommand(user.Id);
            var tokensResponse = await mediator.Send(refcommand, ct);
            jwtGenerator.SetTokensInsideCookie(tokensResponse,HttpContext);
            return Ok();
        }
    [HttpGet("me")]
    public IActionResult Me()
    {
        if (!User.Identity?.IsAuthenticated ?? true)
            return Unauthorized();

        var name = User.Identity!.Name;
        var email = User.FindFirst("email")?.Value;

        return Ok(new { name, email });
    }

    [HttpPost("refresh-Token")]
    public async Task<ActionResult> RefreshTokenAsync(CancellationToken ct)
    {
        var refreshToken = HttpContext.Request.Cookies["authorization-Refresh-Token"];
        if (string.IsNullOrEmpty(refreshToken))
            return Unauthorized("Missing refresh token");

        var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized("Invalid user context");

        var command = new UpdateRefreshTokenCommand(Guid.Parse(userId), refreshToken);
        var tokensResponse = await mediator.Send(command, ct);

        jwtGenerator.SetTokensInsideCookie(tokensResponse, HttpContext);
        return Ok();
    }


}


//I prefer to use Serilog + [Logmasked] attribute to be sure that data can't be leaked  
public sealed record RegisterRequest (string Name,string Email);
public sealed record LoginRequest (string Name,string Email);