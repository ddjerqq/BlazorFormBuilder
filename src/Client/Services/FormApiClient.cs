using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Domain.Aggregates;
using Domain.Common;

namespace Client.Services;

public sealed class FormApiClient(HttpClient http)
{
    private static JsonSerializerOptions JsonOptions = new JsonSerializerOptions
    {
        Converters = { new BaseComponentChoiceConverter() },
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
    };

    public async Task Create(UserForm form, CancellationToken ct = default)
    {
        var json = JsonSerializer.Serialize(form, JsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await http.PostAsync("/api/forms/", content, ct);
        response.EnsureSuccessStatusCode();
    }

    public async Task<IEnumerable<UserForm>> GetAllForms(int page, int perPage = 10, CancellationToken ct = default)
    {
        var response = await http.GetAsync($"/api/forms/page={page}&perPage={perPage}", ct);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<UserForm>>(JsonOptions, ct) ?? [];
    }

    public async Task<UserForm?> GetById(Guid id, CancellationToken ct = default)
    {
        var response = await http.GetAsync($"/api/forms/{id}", ct);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<UserForm>(JsonOptions, ct);
    }

    public async Task UpdateForm(UserForm form, CancellationToken ct = default)
    {
        var json = JsonSerializer.Serialize(form, JsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await http.PutAsync("/api/forms/", content, ct);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteForm(Guid id, CancellationToken ct = default)
    {
        var response = await http.DeleteAsync($"/api/forms/{id}", ct);
        response.EnsureSuccessStatusCode();
    }
}