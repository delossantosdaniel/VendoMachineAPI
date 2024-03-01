using Microsoft.EntityFrameworkCore;
using VendoMachineAPI.Models.Domain;

namespace VendoMachineAPI.Data
{
    public class DataContextEF: DbContext
    {
        // Constructor initializes DataContextEF with IConfiguration, allowing access to application configuration.
        private readonly IConfiguration _config;

        public DataContextEF(IConfiguration config)
        {
            _config = config;
        }
        
        
        //Setting up my Model as DbSet
        public DbSet<Item> Items { get; set; }


        //To retry on failure, and configure EF to SQL 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(_config.GetConnectionString("DefaultConnection"),
                    optionsBuilder => optionsBuilder.EnableRetryOnFailure());
            }
        }
        //override and set primary key, and unique index for itemFood
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().HasKey(item => item.itemId);

            modelBuilder.Entity<Item>().HasIndex(item => item.itemFood).IsUnique();
        }
    }
}
