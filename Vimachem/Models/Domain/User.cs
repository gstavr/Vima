namespace Vimachem.Models.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? CreatedDate { get; set; }
        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();

    }
}
