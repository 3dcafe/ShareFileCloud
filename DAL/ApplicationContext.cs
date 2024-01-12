using Entitles.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace DAL;
/// <summary>
/// Application Context DbContext
/// </summary>
public class ApplicationContext : DbContext
{
    /// <summary>
    /// Base constructor
    /// </summary>
    /// <param name="options"></param>
    public ApplicationContext()
    {
    }

    /// <summary>
    /// Base constructor
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
#if DEBUG
        var connectionString = Environment.GetEnvironmentVariable("DefaultConnection");
        string path = @"C:\Source\test.txt";
        if (File.Exists(path) && string.IsNullOrEmpty(connectionString))
        {
            using (FileStream fstream = File.OpenRead(path))
            {
                byte[] buffer = new byte[fstream.Length];
                fstream.Read(buffer, 0, buffer.Length);
                connectionString = Encoding.Default.GetString(buffer);
            }
        }
        optionsBuilder.UseNpgsql(connectionString);
#endif
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