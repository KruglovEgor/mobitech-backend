namespace DBinit.Models
{
public class Installation
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime UpdatedAt { get; set; }
    public ICollection<EquipmentType> EquipmentTypes { get; set; }
}
} 