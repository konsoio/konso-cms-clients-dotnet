namespace Konso.Clients.Cms.Domain
{
    public interface IPublishable
    {
        bool Publish { get; set; }

        long? PublishedOn { get; set; }
    }

}
