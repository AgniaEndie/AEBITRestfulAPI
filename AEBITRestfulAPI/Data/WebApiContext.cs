using AEBITRestfulAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AEBITRestfulAPI.Data;

public class WebApiContext : DbContext
{

    public WebApiContext(DbContextOptions<WebApiContext> configuration) : base(configuration)
    {
        
    }
    
    public DbSet<AuthModels.User> User { get; set; }
}