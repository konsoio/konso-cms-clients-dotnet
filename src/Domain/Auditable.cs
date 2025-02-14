namespace Konso.Clients.Cms.Domain
{
    public abstract class Auditable<TKey> : IAuditable<TKey> where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? PublishedOn { get; set; }
        public string? PublishedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }

        public bool IsNew
        {
            get { return EqualityComparer<TKey>.Default.Equals(Id, default); }
        }
    }

}
