using Konso.Clients.Cms.Domain.Sites;

namespace Konso.Clients.Cms.Domain.Contents
{
    public class KonsoContentFilter : BaseFilter
    {
        public required KonsoCmsSite SiteConfig { get; set; }
        public byte? Type { get; set; }
        public int? Category { get; set; }
        public int? Id { get; set; }
        public bool? IsPublished { get; set; }

        public bool? IsLatest { get; set; }

        public string? Slug { get; set; }

        public string? Term { get; set; }
    }
}
