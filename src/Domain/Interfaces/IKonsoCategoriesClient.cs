using GetInfra.WebApi.Abstractions.Models.Responses;
using Konso.Clients.Cms.Domain.Categories;
using Konso.Clients.Cms.Domain.Sites;

namespace Konso.Clients.Cms.Domain.Interfaces
{
    public interface IKonsoCategoriesClient
    {
        Task<PagedResponse<CategoryDto<int>>> GetByBucketIdAsync(KonsoCmsSite siteConfig, int section, int from, int to);
    }
}
