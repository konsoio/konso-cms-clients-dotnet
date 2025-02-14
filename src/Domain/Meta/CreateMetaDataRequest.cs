using Konso.Clients.Cms.Domain.Enums;

namespace Konso.Clients.Cms.Domain.Meta
{
    public class CreateMetaDataRequest<TKey> : IPublishable<TKey> where TKey : IEquatable<TKey>
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public TKey SiteId { get; set; }

        public TKey ItemId { get; set; }

        public MetaDataTypes Type { get; set; }
        public bool Publish { get; set; }
        public long? PublishedOn { get; set; }
    }

}
