using Konso.Clients.Cms.Domain.Categories;
using Konso.Clients.Cms.Domain.Enums;
using Konso.Clients.Cms.Domain.Meta;
using Konso.Clients.Cms.Domain.Tags;

namespace Konso.Clients.Cms.Domain.Contents
{
    public class ContentDto<TKey> : Auditable<TKey>, IBelong<TKey> where TKey : IEquatable<TKey>
    {

        public ContentDto()
        {
            Categories = new List<CategoryDto<TKey>>();
        }
        public string Name { get; set; }
        public string Body { get; set; }

        public string Title { get; set; }
        public string Announce { get; set; }
        public bool IsActive { get; set; }

        public ContentTypes Type { get; set; }

        public TKey SiteId { get; set; }

        public int? Order { get; set; }
        public List<CategoryDto<TKey>> Categories { get; set; }
        public List<TagDto<TKey>> Tags { get; set; }
        public string PreviewImage { get; set; }
        public bool IsPublished { get { return PublishedOn.HasValue; } }

        public TKey? ParentId { get; set; }

        public int Likes { get; set; }
        public int Shares { get; set; }
        public int Views { get; set; }

        public bool AllowComments { get; set; }

        public string Custom { get; set; }

        public string OriginMedia { get; set; }

        public string? Slug { get; set; }

        public Dictionary<string, MetaDataDto<TKey>>? MetaData { get; set; }
    }

}
