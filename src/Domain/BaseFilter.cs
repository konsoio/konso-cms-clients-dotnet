namespace Konso.Clients.Cms.Domain
{
    public abstract class BaseFilter
    {
        public int From { get; set; } = 0;
        public int To { get; set; } = 1;

        public byte? Sort { get; set; }
    }
}
