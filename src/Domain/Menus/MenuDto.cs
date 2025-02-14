using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konso.Clients.Cms.Domain.Menus
{
    public class MenuDto<TKey> : Auditable<TKey>, IBelong<TKey> where TKey : IEquatable<TKey>
    {
        public List<MenuItemDto<TKey>> Items { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }
        public List<TKey> Pages { get; set; }
        public TKey SiteId { get; set; }

        public int? Order { get; set; }
    }

}
