using Konso.Clients.Cms.Domain.Enums;

namespace Konso.Clients.Cms.Domain.Pages
{
    public class CreatePageComponentRequest<TKey>
    {
        public TKey? Id { get; set; }
        public string? Name { get; set; }
        public string? Body { get; set; }

        /// <summary>
        /// Page can be of following types: 1 - PageComponent, 2 - InlineHTML, 6 - InlineMarkdown
        /// </summary>
        public ContentTypes Type { get; set; }
        public int? Order { get; set; }

        public TKey SiteId { get; set; }
    }

}
