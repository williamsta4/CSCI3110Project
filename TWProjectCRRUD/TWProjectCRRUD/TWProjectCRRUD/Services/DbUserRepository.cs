// using statements
using TWProjectCRRUD.Models.Entities;
using Microsoft.EntityFrameworkCore;
using TWProjectCRRUD.Data;

namespace TWProjectCRRUD.Services;
//--

/// <summary>
/// This is the User Repository
/// </summary>
public class DbUserRepository : IUserRepository
{

    /// <summary>
    /// Injection for the Db Context class
    /// </summary>
    private readonly ApplicationDbContext _db;
    public DbUserRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    /// <summary>
    /// This is the create method for the user's repo 
    /// </summary>
    /// <param name="newUser">A new user object</param>
    /// <returns>The new user is returned</returns>
    public async Task<User> CreateUserAsync(User newUser)
    {
        _db.Users.Add(newUser);
        await _db.SaveChangesAsync();
        return newUser; 
    }
    //--

    /// <summary>
    /// This is the Read method for the user repository
    /// </summary>
    /// <param name="id">The user's id is read in</param>
    /// <returns>The user is returned</returns>
    public async Task<User?> ReadAsync(int id)
    {
        return await _db.Users
            .Include(vu => vu.VehicleUsers)
                .ThenInclude(v => v.Vehicle)
                .FirstOrDefaultAsync(v => v.Id == id);
    }
    //--

    /// <summary>
    /// This is the Read All method for the user repo.
    /// </summary>
    /// <returns>A list of users is returned. </returns>
    public async Task<ICollection<User>> ReadAllAsync()
    {
        return await _db.Users
         .Include(vu => vu.VehicleUsers)
                .ThenInclude(v => v.Vehicle)
                .ToListAsync();
    }
    //--

 
    /// <summary>
    /// This is the Update method for the user repo. 
    /// </summary>
    /// <param name="oldId"> This is the old Id of the user.</param>
    /// <param name="user">This is a new user object. </param>
    /// <returns>N/A</returns>
    public async Task UpdateUserAsync(int oldId, User user)
    {
       User? userToUpdate = await ReadAsync(oldId);
       if (userToUpdate != null)
        {
            userToUpdate.Id = user.Id;
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            await _db.SaveChangesAsync();
        }
    }
    //--

    /// <summary>
    /// This is the Delete method for the user repo. 
    /// </summary>
    /// <param name="id">This is the id of the user.</param>
    /// <returns>N/A</returns>
    public async Task DeleteUserAsync(int id)
    {
        User? userToDelete = await ReadAsync(id);
        if (userToDelete != null)
        {
            _db.Users.Remove(userToDelete);
            await _db.SaveChangesAsync();
        }
    }
    //--
}
