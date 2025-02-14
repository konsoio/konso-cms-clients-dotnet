using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konso.Clients.Cms.Domain
{
    public interface IAuditable<TKey> where TKey : IEquatable<TKey>
    {
        TKey Id { get; set; }
        DateTime CreatedOn { get; set; }
        string CreatedBy { get; set; }

        DateTime? PublishedOn { get; set; }
        string? PublishedBy { get; set; }

        DateTime? ModifiedOn { get; set; }
        string? ModifiedBy { get; set; }

        bool IsNew { get; }
    }

}
