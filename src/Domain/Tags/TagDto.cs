using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konso.Clients.Cms.Domain.Tags
{
    public class TagDto<T> where T : notnull
    {
        public T Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public T SiteId { get; set; }

        public int Count { get; set; }
    }

}
