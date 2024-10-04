namespace Konso.Clients.Cms.Domain
{
    public abstract class BaseFilter
    {
        public int From { get; set; }
        public int To { get; set; }

        public byte? Sort { get; set; }
    }
}
