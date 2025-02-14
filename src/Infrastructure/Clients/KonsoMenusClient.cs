using GetInfra.WebApi.Abstractions.Models.Responses;
using Konso.Clients.Cms.Domain.Interfaces;
using Konso.Clients.Cms.Domain.Menus;
using Konso.Clients.Cms.Domain.Sites;
using Konso.Clients.Cms.Infrastructure.Clients;
using Konso.Clients.Cms.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Web;

namespace Konso.Clients.Cms.Infrastructure.Clients
{
    public class KonsoMenusClient : BaseKonsoClient, IKonsoMenusClient
    {
        private readonly IHttpClientFactory _clientFactory;

        private readonly string _endpoint;



        public KonsoMenusClient(IConfiguration configuration, IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _endpoint = configuration.GetValue<string>("Konso:Cms:Endpoint");
            _endpoint = _endpoint.RemoveTailSlash();

        }
        public async Task<PagedResponse<MenuDto<int>>> GetByBucketIdAsync(KonsoCmsSite siteConfig, int from, int to)
        {
            var client = _clientFactory.CreateClient();


            ValidateConfig(siteConfig, _endpoint);
            if (!client.DefaultRequestHeaders.TryAddWithoutValidation("x-api-key", siteConfig.ApiKey)) throw new Exception("Missing API key");

            var builder = new UriBuilder($"{_endpoint}/menus/{siteConfig.BucketId}");

            var query = HttpUtility.ParseQueryString(builder.Query);

            query["from"] = from.ToString();
            query["to"] = to.ToString();


            builder.Query = query.ToString();
            string url = builder.ToString();

            string responseBody = await client.GetStringAsync(url);

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            var responseObj = JsonSerializer.Deserialize<PagedResponse<MenuDto<int>>>(responseBody, options);
            return responseObj;
        }
    }
}
