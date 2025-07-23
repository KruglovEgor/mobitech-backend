using Microsoft.EntityFrameworkCore;
using DBinit.Models;

public class AppDbContext : DbContext
{
    public DbSet<Installation> Installations { get; set; }
    public DbSet<EquipmentPossibleType> EquipmentPossibleTypes { get; set; }
    public DbSet<EquipmentType> EquipmentTypes { get; set; }
    public DbSet<Equipment> Equipments { get; set; }
    public DbSet<Membership> Memberships { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<MeasurementPoint> MeasurementPoints { get; set; }
    public DbSet<MeasurementHistory> MeasurementHistories { get; set; }
    public DbSet<MeasurementStatus> MeasurementStatuses { get; set; }
    public DbSet<Notification> Notifications { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // NOT NULL по умолчанию для string (если не nullable)
        // Уникальные индексы
        modelBuilder.Entity<Installation>().HasIndex(x => x.Name).IsUnique();
        modelBuilder.Entity<EquipmentPossibleType>().HasIndex(x => x.Name).IsUnique();
        modelBuilder.Entity<Role>().HasIndex(x => x.Name).IsUnique();
        modelBuilder.Entity<MeasurementStatus>().HasIndex(x => x.Name).IsUnique();

        // EquipmentType: связь с Installation и EquipmentPossibleType
        modelBuilder.Entity<EquipmentType>()
            .HasOne(x => x.Installation)
            .WithMany(x => x.EquipmentTypes)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<EquipmentType>()
            .HasOne(x => x.EquipmentPossibleType)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        // Equipment: связь с EquipmentType и Contactor (Membership)
        modelBuilder.Entity<Equipment>()
            .HasOne(x => x.EquipmentType)
            .WithMany(x => x.Equipments)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Equipment>()
            .HasOne(x => x.Contactor)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        // Membership: связь с Role
        modelBuilder.Entity<Membership>()
            .HasOne(x => x.Role)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        // MeasurementPoint: связь с Equipment и MeasurementStatus
        modelBuilder.Entity<MeasurementPoint>()
            .HasOne(x => x.Equipment)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<MeasurementPoint>()
            .HasOne(x => x.MeasurementStatus)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        // MeasurementHistory: связь с MeasurementPoint и User (Membership)
        modelBuilder.Entity<MeasurementHistory>()
            .HasOne(x => x.MeasurementPoint)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<MeasurementHistory>()
            .HasOne(x => x.User)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        // Notification: связь с Membership
        modelBuilder.Entity<Notification>()
            .HasOne(x => x.User)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
    }
} 