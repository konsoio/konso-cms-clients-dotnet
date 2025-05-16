namespace Konso.Clients.Cms.Domain.Menus
{
    public class CreateMenuRequest : IPublishable
    {
        public string Name { get; set; }
        
        public string Description { get; set; }

        public List<CreateMenuItemRequest> Items { get; set; }
        public bool Publish { get; set; }
        public long? PublishedOn { get; set; }
    }

}
