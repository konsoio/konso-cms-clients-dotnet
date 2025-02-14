using Konso.Clients.Cms.Domain.Categories;
using Konso.Clients.Cms.Domain.Contents;
using Konso.Clients.Cms.Domain.Enums;
using Konso.Clients.Cms.Domain.Menus;
using Konso.Clients.Cms.Domain.Meta;
using Newtonsoft.Json;

namespace Konso.Clients.Cms.Domain.Pages
{
    public class PageDto<TKey> : Auditable<TKey>, IBelong<TKey> where TKey : IEquatable<TKey>
    {
        public PageDto() { }
      
        public TKey? ParentId { get; set; }
        public PageTypes PageType { get; set; }
        public TKey? MasterPageId { get; set; }
        public Dictionary<string, MetaDataDto<TKey>> MetaData { get; set; }
        public string Css { get; set; }
        public bool IsActive { get; set; }


        public string Name { get; set; }
        public string Slug { get; set; }

        public string Announce { get; set; }

        public TKey SiteId { get; set; }

        public string Title { get; set; }


        public List<CategoryDto<TKey>> Categories { get; set; }
        public List<ContentDto<TKey>> Contents { get; set; }
        public List<MenuDto<TKey>> Menus { get; set; }

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
        public PageDto<TKey> MasterPage { get; set; }

        public bool IsSystem { get; set; }

        public int Likes { get; set; }
        public int Shares { get; set; }
        public int Views { get; set; }

        public string Custom { get; set; }
    }

}
