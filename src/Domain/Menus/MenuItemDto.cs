using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konso.Clients.Cms.Domain.Menus
{
    public class MenuItemDto<TKey> : Auditable<TKey> where TKey : IEquatable<TKey>
    {
        public TKey MenuId { get; set; }

        public string Name { get; set; }
        public string Text { get; set; }

        public string Alt { get; set; }

        public string Link { get; set; }

        public byte Order { get; set; }

        public bool IsActive { get; set; }

        public string ImagePath { get; set; }

        public string ImagePathAlt { get; set; }

        public TKey? PageId { get; set; }

        public string Rel { get; set; }

        public string Target { get; set; }
    }

}
