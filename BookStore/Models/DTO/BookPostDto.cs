namespace BookStore.Models.DTO
{
    public class BookPostDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Author { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }

        public List<CategoryDto> Categories { get; set; }=new List<CategoryDto>();
    }
}
