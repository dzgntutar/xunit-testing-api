using Microsoft.EntityFrameworkCore;

namespace Workshop.Web.Models
{
    public class SchoolDbContext : DbContext
    {

        public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Department> Department { get; set; }   


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Name).IsRequired();
                e.Property(x => x.Surname).IsRequired();
                e.Property(x => x.Department).IsRequired();
            });

            modelBuilder.Entity<Department>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Name).IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }

    }
}
