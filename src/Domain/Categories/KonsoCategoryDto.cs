namespace Konso.Clients.Cms.Domain.Categories
{
    public class KonsoCategoryDto
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string? Path { get; set; }
        public string? FullName { get; set; }
        public string? ImgUrl { get; set; }
        public int? Section { get; set; }

        public string? Slug { get; set; }
    }
}
