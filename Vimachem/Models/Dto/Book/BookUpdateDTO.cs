using System.ComponentModel.DataAnnotations;

namespace Vimachem.Models.Dto.Book
{
    public class BookUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [StringLength(maximumLength: 30, ErrorMessage = "Book {0} should be between {2} and {1} characters"), MinLength(1)]
        public string Name { get; set; }
        [StringLength(maximumLength: 30, ErrorMessage = "Book {0} should be between {2} and {1} characters"), MinLength(1)]
        public string Description { get; set; }

        // Foreign Key
        public int CategoryId { get; set; }


    }
}
