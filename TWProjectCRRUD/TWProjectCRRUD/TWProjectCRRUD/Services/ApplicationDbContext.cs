using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TWProjectCRRUD.Models.Entities;

namespace TWProjectCRRUD.Data;

public class ApplicationDbContext : IdentityDbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options)
	{
	}
	//setting 
	public DbSet<Vehicle> Vehicles => Set<Vehicle>();
	public DbSet<User> Users => Set<User>();
	public DbSet<VehicleUser> VehicleUsers => Set<VehicleUser>();
}