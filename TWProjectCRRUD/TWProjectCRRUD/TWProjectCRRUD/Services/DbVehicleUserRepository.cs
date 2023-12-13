using TWProjectCRRUD.Models.Entities;
using Microsoft.EntityFrameworkCore;
using TWProjectCRRUD.Data;

namespace TWProjectCRRUD.Services;

/// <summary>
/// 
/// </summary>
public class DbVehicleUserRepository : IVehicleUserRepository
{
    /// <summary>
    /// 
    /// </summary>
    private readonly ApplicationDbContext _db;
    private readonly IVehicleRepository _vehicleRepo;
    private readonly IUserRepository _userRepo;

    public DbVehicleUserRepository(ApplicationDbContext db, IUserRepository userRepo, IVehicleRepository vehicleRepo)
    {
        _db = db;
        _vehicleRepo = vehicleRepo;
        _userRepo = userRepo;
    }
    //--

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="vehicleId"></param>
    /// <returns></returns>
    public async Task<VehicleUser?> CreateAsync(int userId, int vehicleId)
    {
        var user = await _userRepo.ReadAsync(userId);
        if (user == null)
        {
            // The author was not found
            return null;
        }

        var vehicle = await _vehicleRepo.ReadAsync(vehicleId);
        if (vehicle == null) 
        {
            // Book was found
            return null;
        }

        var vehicleUser = new VehicleUser
        {
            VehicleId = vehicleId,
            UserId = userId,
            User = user,
            Vehicle = vehicle
        };
        user.VehicleUsers.Add(vehicleUser);
        vehicle.VehicleUsers.Add(vehicleUser);
        _db.SaveChanges();
        return vehicleUser;
    }
    //--


    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="vehicleUserId"></param>
    /// <returns></returns>
    public async Task RemoveAsync(int userId, int vehicleId)
    {
        var user = await _userRepo.ReadAsync(userId);
        var vehicleUser = user!.VehicleUsers
            .FirstOrDefault(vu => vu.Id == vehicleId);
        var vehicle = vehicleUser!.Vehicle;
        user!.VehicleUsers.Remove(vehicleUser);
        vehicle!.VehicleUsers.Remove(vehicleUser);
        await _db.SaveChangesAsync();
    }
    //--
    
    public async Task RemoveVehicleAsync( int vehicleId, int userId)
    {
        var vehicle = await _vehicleRepo.ReadAsync(vehicleId);
        var userVehicle = vehicle!.VehicleUsers
            .FirstOrDefault(vu => vu.Id == userId);
        var user = userVehicle!.User;
        vehicle!.VehicleUsers.Remove(userVehicle);
        user!.VehicleUsers.Remove(userVehicle);
        await _db.SaveChangesAsync();
    }
}

