using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using EventCachingDemo.Shared;
using MediatR;
using Newtonsoft.Json;

namespace EventCachingDemo.Client;

public class HttpClientWrapper
{
    private readonly HttpClient _httpClient;

    public HttpClientWrapper(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<TResult?> SendAsync<TResult>(IRequest<TResult> query)
    {
        var response = await SendInternal(query);

        if (!response.IsSuccessStatusCode)
        {
            return default;
        }

        return await response.Content.ReadFromJsonAsync<TResult>();
    }

    private async Task<HttpResponseMessage> SendInternal(IBaseRequest request)
    {
        var json = JsonConvert.SerializeObject(new Message(request));

        var message = new HttpRequestMessage(HttpMethod.Post, "Message")
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };

        var response = await _httpClient.SendAsync(message);

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            var errorList = await response.Content.ReadFromJsonAsync<string[]>();
            if (errorList?.Length > 0)
            {
                throw new ValidationException(string.Join("<br/>", errorList));
            }
        }

        return response;
    }
}