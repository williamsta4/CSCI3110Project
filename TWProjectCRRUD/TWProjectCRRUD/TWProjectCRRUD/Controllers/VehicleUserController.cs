using TWProjectCRRUD.Models.Entities;
using TWProjectCRRUD.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace TWProjectCRRUD.Controllers;

public class VehicleUserController : Controller
{
    private readonly IVehicleRepository _vehicleRepo;
    private IUserRepository _userRepo;
    private IVehicleUserRepository _vehicleUserRepo;

    public VehicleUserController(IVehicleRepository vehicleRepo, IUserRepository userRepo, IVehicleUserRepository vehicleUserRepo)
    {
        _vehicleRepo = vehicleRepo;
        _userRepo = userRepo;
        _vehicleUserRepo = vehicleUserRepo;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IActionResult Index()
    {
        return View();
    }
    //--

    /// <summary>
    /// 
    /// </summary>
    /// <param name="vehicleId"></param>
    /// <returns></returns>
    public async Task<IActionResult> AssignUser([Bind(Prefix = "id")] int vehicleId)
    {
        var vehicle = await _vehicleRepo.ReadAsync(vehicleId);
        ViewData["Vehicle"] = vehicle;
        var allUsers = await _userRepo.ReadAllAsync();
        var registeredVehicleUsers = vehicle.VehicleUsers
                .Select(vu => vu.User).ToList();

        var allOtherUsers = allUsers.Except(registeredVehicleUsers);
        return View(allOtherUsers);
    }
    //--

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<IActionResult> AssignVehicle([Bind(Prefix = "id")] int userId)
    {
        var user = await _userRepo.ReadAsync(userId);
        ViewData["User"] = user;
        var allVehicles = await _vehicleRepo.ReadAllAsync();
        var registeredUserVehicles = user.VehicleUsers
                .Select(vu => vu.Vehicle).ToList();

        var allOtherVehicles = allVehicles.Except(registeredUserVehicles);
        return View(allOtherVehicles);
    }
    //--

    /// <summary>
    /// 
    /// </summary>
    /// <param name="vehicleId"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<IActionResult> AssignToVehicle([Bind(Prefix = "id")] int vehicleId, int userId)
    {
        await _vehicleUserRepo.CreateAsync(userId, vehicleId);
        return RedirectToAction("Index", "Vehicle");
    }
    //--

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="vehicleId"></param>
    /// <returns></returns>
    public async Task<IActionResult> AssignToUser([Bind(Prefix = "id")] int userId, int vehicleId)
    {
        await _vehicleUserRepo.CreateAsync(userId, vehicleId);
        return RedirectToAction("Index", "User");
    }
    //--


    public async Task<IActionResult> RemoveUserList([Bind(Prefix = "id")] int vehicleId)
    {
        var vehicle = await _vehicleRepo.ReadAsync(vehicleId);
        ViewData["Vehicle"] = vehicle;
        var link = vehicle.VehicleUsers
            .FirstOrDefault(vu => vu.VehicleId == vehicleId);
        ViewData["link"] = link;
        var allUsers = await _userRepo.ReadAllAsync();
        var registeredVehicleUsers = vehicle.VehicleUsers
                .Select(vu => vu.User).ToList();


        return View(registeredVehicleUsers);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="vehicleId"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<IActionResult> RemoveVehicleList([Bind(Prefix = "id")] int userId)
    {
        var user = await _userRepo.ReadAsync(userId);
        ViewData["User"] = user;
        var link = user.VehicleUsers
            .FirstOrDefault(vu => vu.UserId == userId);
        ViewData["link"] = link;
        var allVehicles = await _vehicleRepo.ReadAllAsync();
        var registeredUserVehicles = user.VehicleUsers
                .Select(vu => vu.Vehicle).ToList();
        return View(registeredUserVehicles);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="vehicleId"></param>
    /// <returns></returns>
    
    public async Task<IActionResult> RemoveConfirmed([Bind(Prefix = "id")] int vehicleUserId, int userId )
    {
        await _vehicleUserRepo.RemoveAsync(userId, vehicleUserId);
        return RedirectToAction("Index", "Vehicle", new { id = userId }); 
    }


    public async Task<IActionResult> RemoveConfirmedVehicle([Bind(Prefix = "id")] int userVehicleId, [Bind(Prefix = "userId")] int vehicleId)
    {
        await _vehicleUserRepo.RemoveVehicleAsync(vehicleId, userVehicleId);
        return RedirectToAction("Index", "User", new { id = vehicleId });
    }
    //--

}
