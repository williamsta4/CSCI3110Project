//Using statements:
using TWProjectCRRUD.Models.Entities;
using TWProjectCRRUD.Services;
using Microsoft.AspNetCore.Mvc;
namespace TWProjectCRRUD.Controllers;
//--

/// <summary>
/// This is the Controller code for the user 
/// </summary>
public class UserController : Controller
{
    
    /// <summary>
    /// This is the injection method for the user repository
    /// </summary>
    private readonly IUserRepository _userRepo;
    public UserController( IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    /// <summary>
    /// This is the read all method for the user
    /// </summary>
    /// <returns>allUsers passed into the view</returns>
    public async Task<IActionResult> Index()
    {
        var allUsers = await _userRepo.ReadAllAsync();
        return View(allUsers);
    }
    //--

    /// <summary>
    /// This is the Create method to create a new user.
    /// </summary>
    /// <param name="newUser">A new user object is paassed in</param>
    /// <returns>It returns the new user passed into the view. </returns>
    public async Task<IActionResult> Create(User newUser)
    {
        if (ModelState.IsValid)
        {
            await _userRepo.CreateUserAsync(newUser);
            return RedirectToAction("Index");
        }
        return View(newUser);
    }
    //--

    /// <summary>
    /// This is the Details method for a user.
    /// </summary>
    /// <param name="id">The user's id is passed in. </param>
    /// <returns>The user Id passed into the view.</returns>
    public async Task<IActionResult> Details(int id)
    {
        var user = await _userRepo.ReadAsync(id);
        if (user == null)
        {
            return RedirectToAction("Index");
        }
        return View(user);
    }
    //--

   /// <summary>
   /// This is the Edit method for the User. 
   /// </summary>
   /// <param name="id">The user id is passed in. </param>
   /// <returns>Returns the user's id passed into the view. </returns>
    public async Task<IActionResult> Edit(int id)
    {
        var user = await _userRepo.ReadAsync(id);
        if (user == null)
        {
            return RedirectToAction("Index");
        }
        return View(user);
    }
   
    /// <summary>
    /// This is the Edit Post method for the user. 
    /// </summary>
    /// <param name="user">A User object is passed in.</param>
    /// <returns>Returns the updated user object.</returns>
    [HttpPost]
    public async Task<IActionResult> Edit(User user)
    {
        if (ModelState.IsValid)
        {
            await _userRepo.UpdateUserAsync(user.Id, user);
            return RedirectToAction("Index");
        }
        return View(user);
    }
    //--

  /// <summary>
  /// This is the Delete method for the user. 
  /// </summary>
  /// <param name="id">The user's id is passed in. </param>
  /// <returns>The user's id passed into the view. </returns>
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _userRepo.ReadAsync(id);
        if (user == null)
        {
            return RedirectToAction("Index");
        }
        return View(user);
    }

    /// <summary>
    /// This is the Delete POST method for the user
    /// </summary>
    /// <param name="id"> The user's id is passed in. </param>
    /// <returns>Redirect's the user back to the Index page. </returns>
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _userRepo.DeleteUserAsync(id);
        return RedirectToAction("Index");
    }
    //--
}
