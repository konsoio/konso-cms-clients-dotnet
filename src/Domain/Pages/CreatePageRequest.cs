using Konso.Clients.Cms.Domain.Enums;
using Konso.Clients.Cms.Domain.Meta;

namespace Konso.Clients.Cms.Domain.Pages
{
    public class CreatePageRequest<TKey> : IPublishable<TKey> where TKey : IEquatable<TKey>
    {
        public CreatePageRequest() { }
        public CreatePageRequest(PageDto<TKey> p)
        {
            Contents = new List<CreatePageComponentRequest<TKey>>();
            Announce = p.Announce;


            IsActive = true;

            Name = p.Name;
            Slug = p.Slug;
            SiteId = p.SiteId;
            Title = p.Title;
            PageType = p.PageType;
            ParentId = p.ParentId;

            MasterPageId = p.MasterPageId;

            if (p.Contents != null)
            {

                foreach (var c in p.Contents)
                    Contents.Add(new CreatePageComponentRequest<TKey>()
                    {
                        Body = c.Body,
                        Name = c.Name,
                        Order = c.Order,
                        Id = c.Id,
                        Type = c.Type
                    });
            }

            CategoryIds = new List<TKey>();
            MetaData = new List<CreateMetaDataRequest<TKey>>();
            if (p.MetaData != null && p.MetaData.Keys.Count > 0)
            {
                foreach (var key in p.MetaData.Keys)
                {
                    MetaData.Add(new CreateMetaDataRequest<TKey> { Key = key, Type = MetaDataTypes.Page, Value = p.MetaData[key].Value, SiteId = p.SiteId });
                }
            }
        }
        public TKey? ParentId { get; set; }
        public PageTypes PageType { get; set; }
        public TKey? MasterPageId { get; set; }
        public List<CreateMetaDataRequest<TKey>> MetaData { get; set; }
        public string Css { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }

        public string Announce { get; set; }

        public TKey SiteId { get; set; }

        public string Title { get; set; }

        public List<CreatePageComponentRequest<TKey>> Contents { get; set; }
        public List<TKey> Menus { get; set; }
        public List<TKey> CategoryIds { get; set; }

        public bool Publish { get; set; }
        public long? PublishedOn { get; set; }

        public string? Custom { get; set; }
    }

}
