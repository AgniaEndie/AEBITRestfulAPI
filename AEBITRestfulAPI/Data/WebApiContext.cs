using AEBITRestfulAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AEBITRestfulAPI.Data;

public class WebApiContext : DbContext
{

    public WebApiContext(DbContextOptions<WebApiContext> configuration) : base(configuration)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseSerialColumns();
    }
    public DbSet<AuthModels.User> User { get; set; }
    public DbSet<AuthModels.Post> Post { get; set; }
}