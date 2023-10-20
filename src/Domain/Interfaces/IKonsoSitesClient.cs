using Konso.Clients.Cms.Domain.Sites;

namespace Konso.Clients.Cms.Domain.Interfaces
{
    public interface IKonsoSitesClient
    {
        Task<List<KonsoSiteDto>> GetByBucketIdAsync(KonsoCmsSite siteConfig);
    }
}
