namespace Konso.Clients.Cms.Domain.Menus
{
    public class UpdateMenuRequest : IPublishable
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<UpdateMenuItemRequest> Items { get; set; }

        public bool Publish { get; set; }
        public long? PublishedOn { get; set; }
    }

}
