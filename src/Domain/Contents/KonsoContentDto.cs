namespace Konso.Clients.Cms.Domain.Contents
{
    public class KonsoContentDto : ContentDto<int>
    {
        public List<string> ImagePaths { get; set; }

        public string ExternalLink { get; set; }
    }
}
