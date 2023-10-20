namespace Konso.Clients.Cms.Domain.Sites
{
    public class KonsoCmsSite
    {
        public string Name { get; set; }
        public string BucketId { get; set; }
        public string ApiKey { get; set; }

        public bool IsDefault { get; set; }

        public string? ParentBucket { get; set; }

        public string Language { get; set; }

        public string Host { get; set; }

        public string AdminEmail { get; set; }
    }
}
