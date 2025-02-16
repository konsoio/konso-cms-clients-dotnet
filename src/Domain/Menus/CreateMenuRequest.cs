using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konso.Clients.Cms.Domain.Menus
{
    public class CreateMenuRequest<TKey> : IPublishable<TKey> where TKey : IEquatable<TKey>
    {
        public string Name { get; set; }
        public TKey SiteId { get; set; }

        public string Description { get; set; }

        public List<CreateMenuItemRequest<TKey>> Items { get; set; }
        public bool Publish { get; set; }
        public long? PublishedOn { get; set; }
    }

}
