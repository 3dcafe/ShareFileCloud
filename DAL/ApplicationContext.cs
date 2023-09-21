using Entites.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL;
/// <summary>
/// Application Context DbContext
/// </summary>
public class ApplicationContext : DbContext
{
    /// <summary>
    /// Base contructor
    /// </summary>
    /// <param name="options"></param>
    public ApplicationContext()
    {
    }

    /// <summary>
    /// Base contructor
    /// </summary>
    /// <param name="options"></param>
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    /// <summary>
    /// On Configuring for migration
    /// </summary>
    /// <param name="optionsBuilder"></param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
#warning set env from config file for build
    }
    /// <summary>
    /// Users on system
    /// </summary>
    public DbSet<ApplicationUser>? Users { get; set; }
    /// <summary>
    /// Files on system on CDN
    /// </summary>
    public DbSet<AppFile>? AppFiles { get; set; }
}