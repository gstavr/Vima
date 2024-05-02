using System.ComponentModel.DataAnnotations;
using Vimachem.Models.Domain;
using Vimachem.Models.Dto.Role;

namespace Vimachem.Models.Dto.User
{
    public class UserUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
