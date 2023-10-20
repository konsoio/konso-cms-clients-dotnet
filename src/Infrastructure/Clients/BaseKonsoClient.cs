using Konso.Clients.Cms.Domain.Sites;

namespace Konso.Clients.Cms.Infrastructure.Clients
{
    public abstract class BaseKonsoClient
    {
        internal void ValidateConfig(KonsoCmsSite siteConfig, string endpoint)
        {
            if (string.IsNullOrEmpty(endpoint)) throw new Exception("Endpoint is not defined");
            if (string.IsNullOrEmpty(siteConfig.BucketId)) throw new Exception("Bucket is not defined");
            if (string.IsNullOrEmpty(siteConfig.ApiKey)) throw new Exception("API key is not defined");
        }
    }
}
