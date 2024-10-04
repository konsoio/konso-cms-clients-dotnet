using GetCms.Models.Contents.Requests;
using GetInfra.WebApi.Abstractions.Models.Responses;
using GetInfra.WebApi.Abstractions.Models;
using Konso.Clients.Cms.Domain.Sites;
using Konso.Clients.Cms.Domain.Contents;

namespace Konso.Clients.Cms.Domain.Interfaces
{
    public interface IKonsoContentsClient
    {
        Task<PagedResponse<KonsoContentDto>> GetByBucketIdAsync(KonsoContentFilter filter);

        Task<GenericResultResponse<int>> CreateAsync(CreateContentRequest<int> request, KonsoCmsSite siteConfig);
        Task<KonsoContentDto> GetByIdAsync(KonsoCmsSite siteConfig, int contentId);
    }
}
