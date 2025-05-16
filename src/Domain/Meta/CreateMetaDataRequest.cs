using Konso.Clients.Cms.Domain.Enums;

namespace Konso.Clients.Cms.Domain.Meta
{
    public class CreateMetaDataRequest : IPublishable
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public int ItemId { get; set; }

        public MetaDataTypes Type { get; set; }
        public bool Publish { get; set; }
        public long? PublishedOn { get; set; }
    }

}
