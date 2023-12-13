using System.ComponentModel.DataAnnotations;

namespace TWProjectCRRUD.Models.Entities;

public class Vehicle
{
    public int Id { get; set; }
    [StringLength(256)]
    [Required]
    public string Model { get; set; } = String.Empty;
    public int ProductionYear { get; set; }
    public ICollection<VehicleUser>? VehicleUsers { get; set; }
}
