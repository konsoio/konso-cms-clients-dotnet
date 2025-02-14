using Konso.Clients.Cms.Domain.Enums;
using Konso.Clients.Cms.Domain.Pages;

namespace Konso.Clients.Cms.Domain.Contents
{
    public class CreateContentRequest<TKey> : IPublishable<TKey> where TKey : IEquatable<TKey>
    {
        public CreateContentRequest() { }
        public CreateContentRequest(ContentDto<TKey> c)
        {
            Announce = c.Announce;
            Body = c.Body;
            IsActive = c.IsActive;
            Name = c.Name;
            Order = c.Order;
            SiteId = c.SiteId;
            Title = c.Title;
            Type = c.Type;
            CategoriesIds = new List<TKey>();
            PreviewImage = c.PreviewImage;
            OriginMedia = c.OriginMedia;
            AllowComments = c.AllowComments;
            Custom = c.Custom;

            if (c.Categories != null)
                foreach (var category in c.Categories)
                    CategoriesIds.Add(category.Id);
            ParentId = c.ParentId;
            Slug = c.Slug;
        }

       

        public CreateContentRequest(CreatePageComponentRequest<TKey> c)
        {
            Body = c.Body;
            Name = c.Name;
            Order = c.Order;
            Type = c.Type;
            CategoriesIds = new List<TKey>();

        }

        public string Name { get; set; }
        public string Body { get; set; }

        public string Title { get; set; }
        public string Announce { get; set; }
        public bool IsActive { get; set; }

        public ContentTypes Type { get; set; }

        public TKey SiteId { get; set; }

        public int? Order { get; set; }
        public List<TKey>? CategoriesIds { get; set; }

        public string PreviewImage { get; set; }

        public List<BasicRequest<TKey>> Tags { get; set; }

        public TKey? ParentId { get; set; }

        public bool AllowComments { get; set; }

        public string Custom { get; set; }

        public string OriginMedia { get; set; }
        public bool Publish { get; set; }
        public long? PublishedOn { get; set; }

        public string? Slug { get; set; }
    }

}
