using Konso.Clients.Cms.Domain.Interfaces;
using Konso.Clients.Cms.Domain.Sites;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace Konso.Clients.Cms.Infrastructure.Clients
{

    public class KonsoSitesClient : BaseKonsoClient, IKonsoSitesClient
    {
        private readonly IHttpClientFactory _clientFactory;

        private readonly string _endpoint;



        public KonsoSitesClient(IConfiguration configuration, IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _endpoint = configuration.GetValue<string>("Konso:Cms:Endpoint");

        }
        public async Task<List<KonsoSiteDto>> GetByBucketIdAsync(KonsoCmsSite siteConfig)
        {
            var client = _clientFactory.CreateClient();


            ValidateConfig(siteConfig, _endpoint);
            if (!client.DefaultRequestHeaders.TryAddWithoutValidation("x-api-key", siteConfig.ApiKey)) throw new Exception("Missing API key");

            var builder = new UriBuilder($"{_endpoint}/sites/{siteConfig.BucketId}");

            string url = builder.ToString();

            string responseBody = await client.GetStringAsync(url);

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            var responseObj = JsonSerializer.Deserialize<KonsoSiteDto>(responseBody, options);
            return new List<KonsoSiteDto>() { responseObj };
        }
    }
}
