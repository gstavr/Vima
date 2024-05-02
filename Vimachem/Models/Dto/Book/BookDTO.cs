using System.ComponentModel.DataAnnotations;

namespace Vimachem.Models.Dto.Book
{
    public class BookDTO
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Foreign Key
        public int CategoryId { get; set; }
        
    }
}
