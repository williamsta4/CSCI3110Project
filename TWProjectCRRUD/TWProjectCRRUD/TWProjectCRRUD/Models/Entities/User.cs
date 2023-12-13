using System.ComponentModel.DataAnnotations;

namespace TWProjectCRRUD.Models.Entities;

public class User
{
    public int Id { get; set; }
    [StringLength(128)]
    public string? FirstName { get; set; }
    [StringLength(128)]
    [Required]
    public string LastName { get; set; } = String.Empty;
    public ICollection<VehicleUser>? VehicleUsers { get; set; }
  
}
