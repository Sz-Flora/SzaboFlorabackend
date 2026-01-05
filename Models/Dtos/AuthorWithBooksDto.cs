namespace SzabóFlórabackend.Models.Dtos
{
    public class AuthorWithBooksDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<BookDto>? Books { get; set; }
    }
}
