using GetInfra.WebApi.Abstractions.Models.Responses;
using GetInfra.WebApi.Abstractions.Models;
using Konso.Clients.Cms.Domain.Sites;
using Konso.Clients.Cms.Domain.Pages;

namespace Konso.Clients.Cms.Domain.Interfaces
{
    public interface IKonsoPagesClient
    {
        Task<PagedResponse<KonsoPageDto>> GetByBucketIdAsync(KonsoCmsSite siteConfig, byte? pageType, string slug, int? id, int from, int to);
        Task<GenericResultResponse<int>> CreateAsync(CreatePageRequest request, KonsoCmsSite siteConfig);
        Task<KonsoPageDto> GetBySlugAsync(KonsoCmsSite siteConfig, string slug);
    }
}
