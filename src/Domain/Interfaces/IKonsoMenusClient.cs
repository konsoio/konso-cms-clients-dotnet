using GetInfra.WebApi.Abstractions.Models.Responses;
using Konso.Clients.Cms.Domain.Menus;
using Konso.Clients.Cms.Domain.Sites;

namespace Konso.Clients.Cms.Domain.Interfaces
{
    public interface IKonsoMenusClient
    {
        Task<PagedResponse<MenuDto<int>>> GetByBucketIdAsync(KonsoCmsSite siteConfig, int from, int to);
    }
}
