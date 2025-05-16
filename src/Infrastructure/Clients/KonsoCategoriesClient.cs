using System.Text.Json;
using System.Web;
using Microsoft.Extensions.Configuration;
using GetInfra.WebApi.Abstractions.Models.Responses;
using Konso.Clients.Cms.Domain.Interfaces;
using Konso.Clients.Cms.Domain.Sites;
using Konso.Clients.Cms.Infrastructure.Extensions;
using Konso.Clients.Cms.Domain.Categories;

namespace Konso.Clients.Cms.Infrastructure.Clients;

public class KonsoCategoriesClient : BaseKonsoClient, IKonsoCategoriesClient
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly string _endpoint;



    public KonsoCategoriesClient(IConfiguration configuration, IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _endpoint = configuration.GetValue<string>("Konso:Cms:Endpoint");
        _endpoint = _endpoint.RemoveTailSlash();

    }
    public async Task<PagedResponse<KonsoCategoryDto>> GetByBucketIdAsync(KonsoCmsSite siteConfig, int? section, int from, int to)
    {
        var client = _clientFactory.CreateClient();
        var result = new PagedResponse<KonsoCategoryDto>(); 

        ValidateConfig(siteConfig, _endpoint);
        if (!client.DefaultRequestHeaders.TryAddWithoutValidation("x-api-key", siteConfig.ApiKey)) throw new Exception("Missing API key");

        var builder = new UriBuilder($"{_endpoint}/cms/categories/{siteConfig.BucketId}");

        var query = HttpUtility.ParseQueryString(builder.Query);

        query["from"] = from.ToString();
        query["to"] = to.ToString();
        if (section.HasValue)
        {
            query["section"] = section.ToString();
        }
       
        builder.Query = query.ToString();
        string url = builder.ToString();

        try
        {

            string responseBody = await client.GetStringAsync(url);

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            result = JsonSerializer.Deserialize<PagedResponse<KonsoCategoryDto>>(responseBody, options);
        }
        catch (HttpRequestException ex)
        {
            return result;
        }
        return result;
    }
}
