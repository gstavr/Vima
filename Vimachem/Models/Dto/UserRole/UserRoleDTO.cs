using System.ComponentModel.DataAnnotations;

namespace Vimachem.Models.Dto
{
    public class UserRoleDTO
    {
    
        [Required]
        public int UserId { get; set; }
        [Required]
        public int RoleId { get; set; }
        [Required]
        public int ExistingRoleId { get; set; }

    }
}
