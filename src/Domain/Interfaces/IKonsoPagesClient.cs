using GetCms.Models.Pages.Dtos;
using GetCms.Models.Pages.Requests;
using GetInfra.WebApi.Abstractions.Models.Responses;
using GetInfra.WebApi.Abstractions.Models;
using Konso.Clients.Cms.Domain.Sites;

namespace Konso.Clients.Cms.Domain.Interfaces
{
    public interface IKonsoPagesClient
    {
        Task<PagedResponse<PageDto<int>>> GetByBucketIdAsync(KonsoCmsSite siteConfig, byte? pageType, string slug, int? id, int from, int to);
        Task<GenericResultResponse<int>> CreateAsync(CreatePageRequest<int> request, KonsoCmsSite siteConfig);
        Task<PageDto<int>> GetBySlugAsync(KonsoCmsSite siteConfig, string slug);
    }
}
