namespace DBinit.Models
{
public class MeasurementPoint
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public float X_0 { get; set; }
    public float X_n_1 { get; set; }
    public float X_lim { get; set; }
    public float V_d { get; set; }
    public DateTime DateNominal { get; set; }
    public DateTime DateFinal { get; set; }
    public DateTime NextFixDate { get; set; }
    public float CoordinateX { get; set; }
    public float CoordinateY { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Equipment Equipment { get; set; }
    public MeasurementStatus MeasurementStatus { get; set; }
}
} 