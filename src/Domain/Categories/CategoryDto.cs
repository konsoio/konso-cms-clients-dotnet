using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konso.Clients.Cms.Domain.Categories
{
    public class CategoryDto<T> where T : notnull
    {
        public T Id { get; set; }
        public T ParentId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string? Path { get; set; }
        public string? FullName { get; set; }
        public string? ImgUrl { get; set; }
        public T SiteId { get; set; }
        public int? Section { get; set; }

        public string? Slug { get; set; }
    }

}
