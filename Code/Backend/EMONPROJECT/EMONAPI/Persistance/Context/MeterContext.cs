using EMONAPI.Persistance.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMONAPI.Persistance.Context
{
    public class MeterContext : DbContext
    {
        public MeterContext(DbContextOptions<MeterContext> options)
            : base(options)
        {

        }
        public MeterContext()
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Data Source=(LocalDb)\\MSSQLLocalDb;Initial Catalog=SmartMeter;Integrated Security=True",
                    builder => builder.EnableRetryOnFailure());
            }
        }

        public DbSet<FullDatagram> datagrams { get; set; }
    }
}
