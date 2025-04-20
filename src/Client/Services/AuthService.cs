using System.Net.Http.Json;
using System.Text;
using Domain.Aggregates;
using Microsoft.AspNetCore.Components.Authorization;
using System.Text.Json;
using System.Security.Claims;

namespace Client.Services;

public sealed class AuthService(HttpClient http, AuthenticationStateProvider stateProvider)
{
    private static readonly JsonSerializerOptions JsonOptions = new()
        { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };


    public async Task Register(User user, CancellationToken ct)
    {
        var json = JsonSerializer.Serialize(user, JsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await http.PostAsync("/api/auth/register", content, ct);
        response.EnsureSuccessStatusCode();
    }


    public async Task<bool> Login(User user, CancellationToken ct)
    {
        var json = JsonSerializer.Serialize(user, JsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await http.PostAsync("/api/auth/login", content, ct);
        if (!response.IsSuccessStatusCode) return false;

        ((CustomAuthStateProvider)stateProvider).NotifyUserAuthentication();
        return true;
    }

}


public class CustomAuthStateProvider(HttpClient http) : AuthenticationStateProvider
{
    private ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var userInfo = await http.GetFromJsonAsync<UserInfo>("/api/auth/me");

            if (userInfo is null)
                return new AuthenticationState(_anonymous);

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, userInfo.Name),
                new(ClaimTypes.Email, userInfo.Email)
            };

            var identity = new ClaimsIdentity(claims, "cookie");
            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }
        catch
        {
            return new AuthenticationState(_anonymous);
        }
    }

    public void NotifyUserAuthentication()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public void NotifyUserLogout()
    {
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
    }
}

public class UserInfo
{
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
}