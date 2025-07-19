public class User
{
    public string UserName { get; set;}
    public Guid UserID { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; } // "Admin", "Editor", etc.
}
