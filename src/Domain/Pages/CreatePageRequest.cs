using Konso.Clients.Cms.Domain.Enums;
using Konso.Clients.Cms.Domain.Meta;

namespace Konso.Clients.Cms.Domain.Pages
{
    public class CreatePageRequest : IPublishable
    {
        public CreatePageRequest() { }
        public CreatePageRequest(KonsoPageDto p)
        {
            Contents = new List<CreatePageComponentRequest>();
            Announce = p.Announce;


            IsActive = true;

            Name = p.Name;
            Slug = p.Slug;
            Title = p.Title;
            PageType = p.PageType;
            ParentId = p.ParentId;

            MasterPageId = p.MasterPageId;

            if (p.Contents != null)
            {

                foreach (var c in p.Contents)
                    Contents.Add(new CreatePageComponentRequest()
                    {
                        Body = c.Body,
                        Name = c.Name,
                        Order = c.Order,
                        Id = c.Id,
                        Type = c.Type
                    });
            }

            CategoryIds = new List<int>();
            MetaData = new List<CreateMetaDataRequest>();
            if (p.MetaData != null && p.MetaData.Keys.Count > 0)
            {
                foreach (var key in p.MetaData.Keys)
                {
                    MetaData.Add(new CreateMetaDataRequest { Key = key, Type = MetaDataTypes.Page, Value = p.MetaData[key].Value });
                }
            }
        }
        public int? ParentId { get; set; }
        public PageTypes PageType { get; set; }
        public int? MasterPageId { get; set; }
        public List<CreateMetaDataRequest> MetaData { get; set; }
        public string Css { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }

        public string Announce { get; set; }

        public string Title { get; set; }

        public List<CreatePageComponentRequest> Contents { get; set; }
        public List<int> Menus { get; set; }
        public List<int> CategoryIds { get; set; }

        public bool Publish { get; set; }
        public long? PublishedOn { get; set; }

        public string? Custom { get; set; }
    }

}
