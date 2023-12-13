using TWProjectCRRUD.Models.Entities;

namespace TWProjectCRRUD.Services;

public interface IVehicleUserRepository
{
    Task<VehicleUser?> CreateAsync(int Id, int vehicleId);
    Task RemoveAsync(int userId, int userVehicleId);
    Task RemoveVehicleAsync (int userId, int vehicleId);
}
