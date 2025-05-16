using Konso.Clients.Cms.Domain.Enums;
using Konso.Clients.Cms.Domain.Pages;

namespace Konso.Clients.Cms.Domain.Contents
{
    public class CreateContentRequest : IPublishable
    {
        public CreateContentRequest() { }
        public CreateContentRequest(KonsoContentDto c)
        {
            Announce = c.Announce;
            Body = c.Body;
            IsActive = c.IsActive;
            Name = c.Name;
            Order = c.Order;
            Title = c.Title;
            Type = c.Type;
            CategoriesIds = new List<int>();
            PreviewImage = c.PreviewImage;
            OriginMedia = c.OriginMedia;
            AllowComments = c.AllowComments;
            Custom = c.Custom;

            if (c.Categories != null)
                foreach (var category in c.Categories)
                    CategoriesIds.Add(category.Id);
            ParentId = c.ParentId ?? 0;
            Slug = c.Slug;
        }

       

        public CreateContentRequest(CreatePageComponentRequest c)
        {
            Body = c.Body;
            Name = c.Name;
            Order = c.Order;
            Type = c.Type;
            CategoriesIds = new List<int>();

        }

        public string Name { get; set; }
        public string Body { get; set; }

        public string Title { get; set; }
        public string Announce { get; set; }
        public bool IsActive { get; set; }

        public ContentTypes Type { get; set; }

        public int? Order { get; set; }
        public List<int>? CategoriesIds { get; set; }

        public string PreviewImage { get; set; }

        public List<BasicRequest> Tags { get; set; }

        public int ParentId { get; set; }

        public bool AllowComments { get; set; }

        public string Custom { get; set; }

        public string OriginMedia { get; set; }
        public bool Publish { get; set; }
        public long? PublishedOn { get; set; }

        public string? Slug { get; set; }
    }

}
