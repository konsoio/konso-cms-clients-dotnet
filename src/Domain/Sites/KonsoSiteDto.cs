namespace Konso.Clients.Cms.Domain.Sites
{
    public class KonsoSiteDto
    {
        public int Id { get; set; }
        public string Language { get; set; }

        public string Name { get; set; }

        public string Host { get; set; }

        public string Charset { get; set; }

        public string PageTitleSeparator { get; set; }

        public int? ParentId { get; set; }
        public string StaticContentUrl { get; set; }
        public string ResourcesUrl { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? PublishedOn { get; set; }

        public string PublishedBy { get; set; }



        public string ModifiedBy { get; set; }
        public DateTime LastUpdated
        {
            get
            {
                if (ModifiedOn.HasValue)
                    return ModifiedOn.Value;

                return CreatedOn;
            }
        }

        public KonsoSiteProperties Config { get; set; }


        public string BucketId { get; set; }

        public List<string> Redirects { get; set; }

        public bool IsDefault { get; set; }
    }
}
