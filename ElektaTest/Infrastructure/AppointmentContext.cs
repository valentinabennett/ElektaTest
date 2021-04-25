using ElektaTest.Domain;
using Microsoft.EntityFrameworkCore;


namespace ElektaTest.Infrastructure
{
    public class AppointmentContext : DbContext
    {
        public AppointmentContext(DbContextOptions<AppointmentContext> options)
      : base(options)
        {
        }

        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
           => options.UseSqlite(@"Data Source=sqliteDB1.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        }
    }
}
