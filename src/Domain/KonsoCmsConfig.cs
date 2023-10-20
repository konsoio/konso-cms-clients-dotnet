using Konso.Clients.Cms.Domain.Sites;

namespace Konso.Clients.Cms.Domain
{
    public class KonsoCmsConfig
    {
        public string Endpoint { get; set; }
        public List<KonsoCmsSite> Sites { get; set; }
    }
}
