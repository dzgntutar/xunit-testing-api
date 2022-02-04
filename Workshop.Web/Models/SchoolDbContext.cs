using Microsoft.EntityFrameworkCore;

namespace Workshop.Web.Models
{
    public class SchoolDbContext : DbContext
    {
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Department> Department { get; set; }

        public string DbPath { get; }

        public SchoolDbContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "mydb.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
           => options.UseSqlite($"Data Source={DbPath}");

    }
}
