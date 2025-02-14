using Konso.Clients.Cms.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konso.Clients.Cms.Domain.Meta
{
    public class MetaDataDto<TKey> : Auditable<TKey>, IBelong<TKey> where TKey : IEquatable<TKey>
    {
        public TKey SiteId { get; set; }

        public TKey ItemId { get; set; }
        public string Key { get; set; }

        public string Value { get; set; }

        public MetaDataTypes Type { get; set; }

        public TKey Id { get; set; }
    }

}
