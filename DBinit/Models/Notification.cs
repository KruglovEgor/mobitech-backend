namespace DBinit.Models
{
public class Notification
{
    public int Id { get; set; }
    public string Message { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public Membership User { get; set; }
}
} 