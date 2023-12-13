//using statements
using TWProjectCRRUD.Controllers;
using TWProjectCRRUD.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TWProjectCRRUD.Data;

namespace TWProjectCRRUD.Services;
//--

/// <summary>
/// This is the Vehicle Repo. 
/// </summary>
public class DbVehicleRepository : IVehicleRepository
{
    /// <summary>
    /// This injects the Db Context class
    /// </summary>
    private readonly ApplicationDbContext _db;
  
    public DbVehicleRepository(ApplicationDbContext db)
    {
        _db = db;
        
   
    }
    //--

    /// <summary>
    /// This Creates a new vehicle for the vehicle repo.
    /// </summary>
    /// <param name="newVehicle">A new vehicle object is passed in</param>
    /// <returns>The new vehicle is returned</returns>
    public async Task<Vehicle> CreateVehicleAsync(Vehicle newVehicle)
    {
        _db.Vehicles.Add(newVehicle);
        await _db.SaveChangesAsync();
        return newVehicle;

    }
    //--

    /// <summary>
    /// This is the Read method for the vehicle repo. 
    /// </summary>
    /// <param name="id">The vehicle Id is passed in.</param>
    /// <returns>It returns the vehicle from the id.</returns>
    public async Task<Vehicle?> ReadAsync(int id)
    {
        return await _db.Vehicles
            .Include(vu => vu.VehicleUsers) //This maps it to the object in the linking table. 
                .ThenInclude(u => u.User) //This maps it to the user in the linking table 
                .FirstOrDefaultAsync(v => v.Id == id);
    }
    //--


    /// <summary>
    /// This is the Read All method for the vehicle repo. 
    /// </summary>
    /// <returns>A list of all vehicles is returned.</returns>
    public async Task<ICollection<Vehicle>> ReadAllAsync()
    {
        return await _db.Vehicles
         .Include(vu => vu.VehicleUsers) //This maps it to the object in the linking table. 
                .ThenInclude(u => u.User) //This maps it to the user in the linking table 
                .ToListAsync();
    }
    //--


    /// <summary>
    /// This is the Update method for the vehicle repo. 
    /// </summary>
    /// <param name="oldId">This is the old id of vehicle.</param>
    /// <param name="vehicle"> This is a new vehicle object.</param>
    /// <returns>N/A</returns>
    public async Task UpdateVehicleAsync (int oldId, Vehicle vehicle)
    {
        Vehicle? vehicleToUpdate = await ReadAsync(oldId);
        if (vehicleToUpdate != null)
        {
            vehicleToUpdate.Model = vehicle.Model;
            vehicleToUpdate.ProductionYear = vehicle.ProductionYear;
            await _db.SaveChangesAsync();
        }
    }
    //--

    /// <summary>
    /// This is the Delete method for the vehicle repo. 
    /// </summary>
    /// <param name="id">The vehicle Id is passed in.</param>
    /// <returns>N/A</returns>
    public async Task DeleteVehicleAsync(int id)
    {
        Vehicle? vehicleToDelete = await ReadAsync(id);
        if (vehicleToDelete != null)
        {
			_db.Vehicles.Remove(vehicleToDelete);
			await _db.SaveChangesAsync();
		}
    }
    //--
}
    