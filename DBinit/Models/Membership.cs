namespace DBinit.Models
{
public class Membership
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public DateTime UpdatedAt { get; set; }
    public Role Role { get; set; }
}
} 