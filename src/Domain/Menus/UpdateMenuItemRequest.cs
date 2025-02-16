namespace Konso.Clients.Cms.Domain.Menus
{
    public class UpdateMenuItemRequest<TKey> : CreateMenuItemRequest<TKey> where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
    }

}
