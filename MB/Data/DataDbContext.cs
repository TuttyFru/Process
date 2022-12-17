using Microsoft.EntityFrameworkCore;

namespace MB.Data
{
    public class DataDbContext : DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
        {
        }
        public DbSet<Address> Address { get; set; }
        public DbSet<Car> Car { get; set; }
        public DbSet<Cargo> Cargo { get; set; }
        public DbSet<Driver> Driver { get; set; }
        public DbSet<Route> Route { get; set; }
        public DbSet<Shipping> Shipping { get; set; }
    }
}
