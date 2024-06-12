using GetCms.Models.Pages.Dtos;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Web;
using GetInfra.WebApi.Abstractions.Models;
using System.Text;
using GetCms.Models.Pages.Requests;
using GetInfra.WebApi.Abstractions.Extentions;
using GetInfra.WebApi.Abstractions.Models.Responses;
using Konso.Clients.Cms.Infrastructure.Clients;
using Konso.Clients.Cms.Domain.Interfaces;
using Konso.Clients.Cms.Domain.Sites;

namespace Yasmin.yaIdentity.Web.Services
{
    public class KonsoPagesClient : BaseKonsoClient, IKonsoPagesClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _endpoint;
 


        public KonsoPagesClient(IConfiguration configuration, IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _endpoint = configuration.GetValue<string>("Konso:Cms:Endpoint");

        }

        public async Task<PagedResponse<PageDto<int>>> GetByBucketIdAsync(KonsoCmsSite siteConfig, byte? pageType, string slug, int? id, int from, int to)
        {
            var client = _clientFactory.CreateClient();


            ValidateConfig(siteConfig, _endpoint);
            if (!client.DefaultRequestHeaders.TryAddWithoutValidation("x-api-key", siteConfig.ApiKey)) throw new Exception("Missing API key");

            var builder = new UriBuilder($"{_endpoint}/pages/{siteConfig.BucketId}");

            var query = HttpUtility.ParseQueryString(builder.Query);

            query["from"] = from.ToString();
            query["to"] = to.ToString();

            if (pageType.HasValue)
                query["type"] = pageType.Value.ToString();


            if (!string.IsNullOrEmpty(slug))
                query["slug"] = slug;

            if (id.HasValue)
                query["id"] = id.Value.ToString();

            builder.Query = query.ToString();
            string url = builder.ToString();

            string responseBody = await client.GetStringAsync(url);

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            var responseObj = JsonSerializer.Deserialize<PagedResponse<PageDto<int>>>(responseBody, options);
            return responseObj;
        }



        public async Task<GenericResultResponse<int>> CreateAsync(CreatePageRequest<int> request, KonsoCmsSite siteConfig)
        {
            var result = new GenericResultResponse<int>();
            var client = _clientFactory.CreateClient();


            ValidateConfig(siteConfig, _endpoint);
            if (!client.DefaultRequestHeaders.TryAddWithoutValidation("x-api-key", siteConfig.ApiKey)) throw new Exception("Missing API key");

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            };

            // serialize request as json
            var jsonStr = JsonSerializer.Serialize(request, options);

            // string content 
            var httpItem = new StringContent(jsonStr, Encoding.UTF8, "application/json");

            // call api
            try
            {
                var response = await client.PostAsync($"{_endpoint}/api/pages/{siteConfig.BucketId}", httpItem);
                var contents = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(contents)) throw new Exception("nothing is back");

                result = JsonSerializer.Deserialize<GenericResultResponse<int>>(contents, options);

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

        public async Task<PageDto<int>> GetBySlugAsync(KonsoCmsSite siteConfig, string slug)
        {
            var client = _clientFactory.CreateClient();


            ValidateConfig(siteConfig, _endpoint);
            if (!client.DefaultRequestHeaders.TryAddWithoutValidation("x-api-key", siteConfig.ApiKey)) throw new Exception("Missing API key");

            //int sortNum = (int)request.Sort;
            var builder = new UriBuilder($"{_endpoint}/api/pages/{siteConfig.BucketId}");
            //{
            //    Port = -1
            //};
            var query = HttpUtility.ParseQueryString(builder.Query);

            query["from"] = "0";
            query["to"] = "1";
            query["slug"] = slug;
            query["isActive"] = "1";
            builder.Query = query.ToString();
            string url = builder.ToString();

            string responseBody = await client.GetStringAsync(url);

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            var responseObj = JsonSerializer.Deserialize<PagedResponse<PageDto<int>>>(responseBody, options);

            if (responseObj.Total == 1)
                return responseObj.List.First();

            return null;
        }
    }
}
