namespace SzabóFlórabackend.Models.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }         
        public string? Title { get; set; }  
        public DateTime PublishDate { get; set; }  
        public int AuthorId { get; set; }    
        public int CategoryId { get; set; }  
    }

}
