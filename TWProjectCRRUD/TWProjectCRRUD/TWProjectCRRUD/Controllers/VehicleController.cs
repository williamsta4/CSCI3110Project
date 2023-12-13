//using statements
using TWProjectCRRUD.Models.Entities;
using TWProjectCRRUD.Services;
using Microsoft.AspNetCore.Mvc;
//using NuGet.Packaging.Signing;
//using System.Collections;
namespace TWProjectCRRUD.Controllers;
//--

/// <summary>
/// This it the controller for the Vehicle
/// </summary>
public class VehicleController : Controller
{

   /// <summary>
   /// This is the injection for the vehicle controller. 
   /// </summary>
    private readonly IVehicleRepository _vehicleRepo;

    public VehicleController(IVehicleRepository vehicleRepo)
    {
            _vehicleRepo = vehicleRepo;
    }
    //--


    /// <summary>
    /// This is the Read All for the Vehciles
    /// </summary>
    /// <returns>a list of all vehicles </returns>
    public async Task<IActionResult> Index()
    {
        var allVehicles = await _vehicleRepo.ReadAllAsync();
        return View(allVehicles);  
    }
    //--
 
    /// <summary>
    /// This is the Details for vehicles. 
    /// </summary>
    /// <param name="id">The vehicle ID is passed in. </param>
    /// <returns>It returns the vehicle passed into the View.</returns>
    public async Task<IActionResult> Details(int id)
    {
        var vehicle = await _vehicleRepo.ReadAsync(id);
        var numberOfUsers = vehicle.VehicleUsers.Count();
        if (vehicle == null)
        {
            return RedirectToAction("Index");
        }
        return View(vehicle);
    }
    //--

    /// <summary>
    /// This is the Create for vehicles. 
    /// </summary>
    /// <param name="newVehicle">A new vehicle object is passed in. </param>
    /// <returns>The new vehicle is passed into the view. </returns>
    public async Task<IActionResult> Create(Vehicle newVehicle)
    {
        if (ModelState.IsValid)
        {
            await _vehicleRepo.CreateVehicleAsync(newVehicle);
            return RedirectToAction("Index");
        }
        return View(newVehicle);
    }
    //--

    /// <summary>
    /// This is the Edit for the vehicle. 
    /// </summary>
    /// <param name="id">This is the vehicle id. </param>
    /// <returns>It returns the updated vehicle id passed into the view.</returns>
    public async Task<IActionResult> Edit(int id)
    {
        var vehicle = await _vehicleRepo.ReadAsync(id);
        if (vehicle == null) 
        { 
            return RedirectToAction("Index"); 
        }
        return View(vehicle);
    }

    /// <summary>
    /// This is the Edit POST for the vehicle. 
    /// </summary>
    /// <param name="vehicle">A vehicle object is passed in.</param>
    /// <returns>It returns the updated vehicle passed into the view. </returns>
    [HttpPost]
    public async Task<IActionResult> Edit(Vehicle vehicle)
    {
        if (ModelState.IsValid) 
        { 
            await _vehicleRepo.UpdateVehicleAsync(vehicle.Id, vehicle);
            return RedirectToAction("Index");
        }
        return View(vehicle);
    }
    //--
    
    /// <summary>
    /// This is the Delete for the vehicle. 
    /// </summary>
    /// <param name="id">The vehicle id is passed in. </param>
    /// <returns>It returns the vehicle id passed into the view. </returns>
    public async Task<IActionResult> Delete (int id)
    {
        var vehicle = await _vehicleRepo.ReadAsync(id);
        if (vehicle == null)
        {
            return RedirectToAction("Index");
        }
        return View(vehicle);
    }

    /// <summary>
    /// This is the Delete POST for the vehicle. 
    /// </summary>
    /// <param name="id">The vehicle id is passed in. </param>
    /// <returns>It redirects to the Index page after confirm is clicked. </returns>
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed (int id)
    {
       await _vehicleRepo.DeleteVehicleAsync(id);
       return RedirectToAction("Index");
    }
    //--
}
