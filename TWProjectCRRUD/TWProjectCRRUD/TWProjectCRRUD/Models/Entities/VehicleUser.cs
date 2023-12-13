using System.Reflection;

namespace TWProjectCRRUD.Models.Entities;

public class VehicleUser
{
    public int Id { get; set; }
    public int VehicleId { get; set; }
    public int UserId { get; set;  }
    public Vehicle? Vehicle { get; set; }
    public User? User { get; set; }
}
