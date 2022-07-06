using System.Net.Http.Json;

namespace Shared;

public static class HttpClientExtensions
{
    public static async Task<TResponse?> PostAsJsonAsync<TRequest, TResponse>(this HttpClient client, string requestUri, TRequest request, CancellationToken cancellationToken = default)
    {
        var responseMessage = await client.PostAsJsonAsync(requestUri, request, cancellationToken);
        responseMessage.EnsureSuccessStatusCode();

        return await responseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken);
    }
}
