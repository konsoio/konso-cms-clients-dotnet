namespace Konso.Clients.Cms.Domain.Menus
{
    public class UpdateMenuRequest<TKey> : IPublishable<TKey> where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }

        public string Name { get; set; }
        public TKey SiteId { get; set; }

        public string Description { get; set; }

        public List<UpdateMenuItemRequest<TKey>> Items { get; set; }

        public bool Publish { get; set; }
        public long? PublishedOn { get; set; }
    }

}
