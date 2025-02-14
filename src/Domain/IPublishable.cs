namespace Konso.Clients.Cms.Domain
{
    public interface IPublishable<TKey> where TKey : IEquatable<TKey>
    {
        bool Publish { get; set; }

        long? PublishedOn { get; set; }
    }

}
