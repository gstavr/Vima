using System.ComponentModel.DataAnnotations;
using Vimachem.Models.Domain;
using Vimachem.Models.Dto.Role;

namespace Vimachem.Models.Dto.User
{
    public class UserCreateDTO
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        [MaxLength(30)]
        public string Surname { get; set; }

        public int RoleId { get; set; } = 2;
    }
}
