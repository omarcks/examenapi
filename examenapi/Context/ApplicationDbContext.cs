using examenapi.Entities;
using Microsoft.EntityFrameworkCore;

namespace examenapi.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Customer> customers { get; set; }
        public DbSet<Product> products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<EstadoRepublica>()
            //     .HasOne(p => p.Persona)
            //     .WithMany(b => b.EstadoRepublica)
            //     .HasForeignKey(p => p.ide);
        }
    }
}

