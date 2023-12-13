//Using statements
using TWProjectCRRUD.Models.Entities;
namespace TWProjectCRRUD.Services;
//--

/// <summary>
/// This is the interface for the Vehicle
/// </summary>
public interface IVehicleRepository
{
    Task<Vehicle> CreateVehicleAsync(Vehicle newVehicle); //creates a new vehicle object
    Task<Vehicle> ReadAsync(int vehicleId); //reads the vehicle from the id
    Task<ICollection<Vehicle>> ReadAllAsync(); // reads a collection of all vehicles
    Task UpdateVehicleAsync(int oldId, Vehicle vehicle); // reads the old ID of the vehicle and a new vehicle object
    Task DeleteVehicleAsync (int id); // deletes the vehicle based upon the id provided. 
}  
//--