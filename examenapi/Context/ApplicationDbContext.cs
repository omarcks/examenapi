using examenapi.Entities;
using Microsoft.EntityFrameworkCore;

namespace apiFabesV2.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Customers> customer { get; set; }
        public DbSet<Products> products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<EstadoRepublica>()
            //     .HasOne(p => p.Persona)
            //     .WithMany(b => b.EstadoRepublica)
            //     .HasForeignKey(p => p.ide);
        }
    }
}

