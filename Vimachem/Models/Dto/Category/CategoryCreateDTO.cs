using System.ComponentModel.DataAnnotations;


namespace Vimachem.Models.Dto.Category
{
    public class CategoryCreateDTO
    {   
        [Required]
        [StringLength(maximumLength: 30, ErrorMessage = "Category {0} should be between {2} and {1} characters"), MinLength(1)]
        public string Name { get; set; }
        public string Description { get; set; }
        
    }
}
