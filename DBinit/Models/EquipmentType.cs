namespace DBinit.Models
{
public class EquipmentType
{
    public int Id { get; set; }
    public float Percent { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Installation Installation { get; set; }
    public EquipmentPossibleType EquipmentPossibleType { get; set; }
    public ICollection<Equipment> Equipments { get; set; }
}
} 