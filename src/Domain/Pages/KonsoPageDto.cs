using Konso.Clients.Cms.Domain.Categories;
using Konso.Clients.Cms.Domain.Contents;
using Konso.Clients.Cms.Domain.Enums;
using Konso.Clients.Cms.Domain.Menus;
using Konso.Clients.Cms.Domain.Meta;
using System.Text.Json.Serialization;

namespace Konso.Clients.Cms.Domain.Pages
{
    public class KonsoPageDto
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? PublishedOn { get; set; }
        public string? PublishedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }


        public int? ParentId { get; set; }
        public PageTypes PageType { get; set; }
        public int? MasterPageId { get; set; }
        public Dictionary<string, MetaDataDto<int>> MetaData { get; set; }
        public string Css { get; set; }
        public bool IsActive { get; set; }


        public string Name { get; set; }
        public string Slug { get; set; }

        public string Announce { get; set; }

        public string Title { get; set; }


        public List<KonsoCategoryDto> Categories { get; set; }
        public List<KonsoContentDto> Contents { get; set; }

        [JsonIgnore]
        public DateTime ActualDate
        {
            get
            {
                if (ModifiedOn.HasValue)
                    return ModifiedOn.Value;
                return CreatedOn;

            }
        }

        [JsonIgnore]
        public KonsoPageDto MasterPage { get; set; }

        public bool IsSystem { get; set; }

        public int Likes { get; set; }
        public int Shares { get; set; }
        public int Views { get; set; }

        public string Custom { get; set; }

        public bool IsNew
        {
            get { return EqualityComparer<int>.Default.Equals(Id, default); }
        }
    }
}
