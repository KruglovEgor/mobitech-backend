namespace DBinit.Models
{
public class Equipment
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public float Percent { get; set; }
    public string Schema { get; set; } = null!;
    public DateTime UpdatedAt { get; set; }
    public EquipmentType EquipmentType { get; set; }
    public Membership Contactor { get; set; }
}
} 