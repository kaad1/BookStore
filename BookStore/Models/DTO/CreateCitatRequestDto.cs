namespace BookStore.Models.DTO
{
    public class CreateCitatRequestDto
    {
        public Guid Id { get; set; }
        public string Citat { get; set; }
        public string Author { get; set; }
        public string Book { get; set; }
    }
}
