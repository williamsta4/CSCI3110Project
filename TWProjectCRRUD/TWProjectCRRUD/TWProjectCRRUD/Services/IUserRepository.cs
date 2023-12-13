using TWProjectCRRUD.Models.Entities;
namespace TWProjectCRRUD.Services;

/// <summary>
/// This it the interface for the User Repository
/// </summary>
public interface IUserRepository
{
    Task<User> CreateUserAsync(User newUser); //creates the user
    Task<User?> ReadAsync(int userId); //reads the user by id
    Task<ICollection<User>> ReadAllAsync(); //reads a collection of all users
    Task UpdateUserAsync(int oldId, User user); //updates the user based on the old id
    Task DeleteUserAsync(int id); //deletes the user by id
}
