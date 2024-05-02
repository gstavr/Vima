using Vimachem.Models.Domain;
using Vimachem.Models.Dto.Role;

namespace Vimachem.Models.Dto.User
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? CreatedDate { get; set; }
        public List<RoleDTO> Roles { get; set; }
    }
}
