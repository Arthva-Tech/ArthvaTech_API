namespace ArthvaTech.API.Models
{
    public class UserViewModel
    {
        public string UserName { get; set; }
        public Guid? UserID { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public Guid RoleID { get; set; }
        public string? RoleName { get; set; }
        public bool? IsActive { get; set; }
    }
}