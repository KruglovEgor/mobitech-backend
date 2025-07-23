namespace DBinit.Models
{
public class MeasurementHistory
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public float Value { get; set; }
    public DateTime UpdatedAt { get; set; }
    public MeasurementPoint MeasurementPoint { get; set; }
    public Membership User { get; set; }
}
} 