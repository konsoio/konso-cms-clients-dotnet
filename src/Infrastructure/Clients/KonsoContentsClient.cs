using GetCms.Models.Contents.Requests;
using GetInfra.WebApi.Abstractions.Extentions;
using GetInfra.WebApi.Abstractions.Models;
using GetInfra.WebApi.Abstractions.Models.Responses;
using Konso.Clients.Cms.Domain.Contents;
using Konso.Clients.Cms.Domain.Interfaces;
using Konso.Clients.Cms.Domain.Sites;
using Konso.Clients.Cms.Infrastructure.Clients;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;
using System.Web;

namespace Yasmin.yaIdentity.Web.Services
{
    public class KonsoContentsClient : BaseKonsoClient, IKonsoContentsClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _endpoint;

        public KonsoContentsClient(IConfiguration configuration, IHttpClientFactory clientFactory)
        {

            _clientFactory = clientFactory;
            _endpoint = configuration.GetValue<string>("Konso:Cms:Endpoint");

        }

        public async Task<PagedResponse<KonsoContentDto>> GetByBucketIdAsync(KonsoCmsSite siteConfig, byte? type, int? category, int? id, int from, int to)
        {
            var client = _clientFactory.CreateClient();


            ValidateConfig(siteConfig, _endpoint);
            if (!client.DefaultRequestHeaders.TryAddWithoutValidation("x-api-key", siteConfig.ApiKey)) throw new Exception("Missing API key");

            var builder = new UriBuilder($"{_endpoint}/api/contents/{siteConfig.BucketId}");

            var query = HttpUtility.ParseQueryString(builder.Query);

            query["from"] = from.ToString();
            query["to"] = to.ToString();

            if (type.HasValue)
                query["type"] = type.Value.ToString();

            if (category.HasValue)
                query["categories"] = category.Value.ToString();

            if(id.HasValue)
                query["id"] = id.Value.ToString();


            builder.Query = query.ToString();
            string url = builder.ToString();

            string responseBody = await client.GetStringAsync(url);

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            var responseObj = JsonSerializer.Deserialize<PagedResponse<KonsoContentDto>>(responseBody, options);
            return responseObj;
        }

        public async Task<GenericResultResponse<int>> CreateAsync(CreateContentRequest<int> request, KonsoCmsSite siteConfig)
        {
            var result = new GenericResultResponse<int>();
            var client = _clientFactory.CreateClient();


            ValidateConfig(siteConfig, _endpoint);
            if (!client.DefaultRequestHeaders.TryAddWithoutValidation("x-api-key", siteConfig.ApiKey)) throw new Exception("Missing API key");

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            // serialize request as json
            var jsonStr = JsonSerializer.Serialize(request, options);

            // string content 
            var httpItem = new StringContent(jsonStr, Encoding.UTF8, "application/json");

            // call api
            try
            {
                var response = await client.PostAsync($"{_endpoint}/api/contents/{siteConfig.BucketId}", httpItem);
                var contents = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(contents)) throw new Exception("nothing is back");

                result =  JsonSerializer.Deserialize<GenericResultResponse<int>>(contents, options);

                if (!result.Succeeded)
                    throw new Exception(string.Format("Error sending value tracking {0}", result.ValidationErrors[0].Message));
                return result;
            }
            catch (HttpRequestException ex)
            {
                result.SafeAddError(new GetInfra.WebApi.Abstractions.ErrorItem(ex.Message, ex.StackTrace));
                return result;
            }
        }

        public async Task<KonsoContentDto> GetByIdAsync(KonsoCmsSite siteConfig, int contentId)
        {
            var paged = await GetByBucketIdAsync(siteConfig, null, null, contentId, 0, 1);

            if (paged.Total > 0)
            {
                return paged.List.First();
            }

            return null;
        }
    }
}
