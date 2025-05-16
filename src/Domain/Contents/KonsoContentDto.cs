using Konso.Clients.Cms.Domain.Categories;
using Konso.Clients.Cms.Domain.Enums;
using Konso.Clients.Cms.Domain.Meta;
using Konso.Clients.Cms.Domain.Tags;

namespace Konso.Clients.Cms.Domain.Contents
{
    public class KonsoContentDto 
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? PublishedOn { get; set; }
        public string? PublishedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }

        public bool IsNew
        {
            get { return EqualityComparer<int>.Default.Equals(Id, default); }
        }

        public KonsoContentDto()
        {
            Categories = new List<KonsoCategoryDto>();
            MetaData = new Dictionary<string, KonsoMetaDataDto>();
        }
        public string Name { get; set; }
        public string Body { get; set; }

        public string Title { get; set; }
        public string Announce { get; set; }
        public bool IsActive { get; set; }

        public ContentTypes Type { get; set; }

        public int? Order { get; set; }
        public List<KonsoCategoryDto> Categories { get; set; }
        public List<TagDto> Tags { get; set; }
        public string PreviewImage { get; set; }
        public bool IsPublished { get { return PublishedOn.HasValue; } }

        public int? ParentId { get; set; }

        public int Likes { get; set; }
        public int Shares { get; set; }
        public int Views { get; set; }

        public bool AllowComments { get; set; }

        public string Custom { get; set; }

        public string OriginMedia { get; set; }

        public string? Slug { get; set; }

        public Dictionary<string, KonsoMetaDataDto> MetaData { get; set; }

        public List<string> ImagePaths { get; set; }

        public string ExternalLink { get; set; }
    }
}
