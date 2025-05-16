using GetInfra.WebApi.Abstractions.Extentions;
using GetInfra.WebApi.Abstractions.Models;
using GetInfra.WebApi.Abstractions.Models.Responses;
using Konso.Clients.Cms.Domain.Contents;
using Konso.Clients.Cms.Domain.Interfaces;
using Konso.Clients.Cms.Domain.Sites;
using Konso.Clients.Cms.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;
using System.Web;

namespace Konso.Clients.Cms.Infrastructure.Clients
{
    public class KonsoContentsClient : BaseKonsoClient, IKonsoContentsClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _endpoint;

        public KonsoContentsClient(IConfiguration configuration, IHttpClientFactory clientFactory)
        {

            _clientFactory = clientFactory;
            _endpoint = configuration.GetValue<string>("Konso:Cms:Endpoint");
            _endpoint = _endpoint.RemoveTailSlash();

        }

        public async Task<PagedResponse<KonsoContentDto>> GetByBucketIdAsync(KonsoContentFilter filter)
        {
            var client = _clientFactory.CreateClient();


            ValidateConfig(filter.SiteConfig, _endpoint);
            if (!client.DefaultRequestHeaders.TryAddWithoutValidation("x-api-key", filter.SiteConfig.ApiKey)) throw new Exception("Missing API key");

            var builder = new UriBuilder($"{_endpoint}/v2/cms/contents/{filter.SiteConfig.BucketId}");

            var query = HttpUtility.ParseQueryString(builder.Query);

            query["from"] = filter.From.ToString();
            query["to"] = filter.To.ToString();

            if (filter.Type.HasValue)
                query["type"] = filter.Type.Value.ToString();

            if (filter.Category.HasValue)
                query["category"] = filter.Category.Value.ToString();

            if(filter.Id.HasValue)
                query["contentId"] = filter.Id.Value.ToString();

            if (filter.Sort.HasValue)
                query["sort"] = filter.Sort.Value.ToString();

            if (filter.IsPublished.HasValue)
                query["isPublished"] = filter.IsPublished.Value.ToString();

            if (!string.IsNullOrEmpty(filter.Slug))
                query["slug"] = filter.Slug;

            if (!string.IsNullOrEmpty(filter.Term))
                query["q"] = filter.Term;

            if (filter.IsLatest.HasValue)
                query["isLatest"] = filter.IsLatest.Value.ToString();

            builder.Query = query.ToString();
            string url = builder.ToString();

            string responseBody = await client.GetStringAsync(url);

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            };

            var responseObj = JsonSerializer.Deserialize<PagedResponse<KonsoContentDto>>(responseBody, options);
            return responseObj;
        }

        public async Task<GenericResultResponse<int>> CreateAsync(CreateContentRequest request, KonsoCmsSite siteConfig)
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
            string jsonStr = JsonSerializer.Serialize(request, options);

            // string content 
            var httpItem = new StringContent(jsonStr, Encoding.UTF8, "application/json");

            // call api
            try
            {
                var response = await client.PostAsync($"{_endpoint}/cms/contents/{siteConfig.BucketId}", httpItem);
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
            var paged = await GetByBucketIdAsync(new KonsoContentFilter() { 
                SiteConfig = siteConfig, 
                Id = contentId ,
                From = 0,
                To = 1,
            });

            if (paged.Total > 0)
            {
                return paged.List.First();
            }

            return null;
        }

        public async Task<KonsoContentDto> GetBySlugAsync(KonsoCmsSite siteConfig, string slug)
        {
            var paged = await GetByBucketIdAsync(new KonsoContentFilter()
            {
                SiteConfig = siteConfig,
                Slug = slug,
                From = 0,
                To = 1,
            });

            if (paged.Total > 0)
            {
                return paged.List.First();
            }

            return null;
        }
    }
}
