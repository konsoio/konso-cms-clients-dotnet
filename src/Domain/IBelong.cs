namespace Konso.Clients.Cms.Domain
{
    public interface IBelong<TKey> where TKey : IEquatable<TKey>
    {
        TKey SiteId { get; set; }
    }

}
