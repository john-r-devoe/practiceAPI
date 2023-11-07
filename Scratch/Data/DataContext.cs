using Microsoft.EntityFrameworkCore;
using Scratch.Models;

namespace Scratch.Data
{
    public class DataContext : DbContext
    {
        private readonly DbContextOptions<DataContext> _options;
        public DataContext(DbContextOptions<DataContext> options)
         : base(options)
        {
            _options = options;
        }

        public virtual DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Practice");
            modelBuilder.Entity<Person>().ToTable("Persons", "Practice").HasKey(u =>
            u.PersonID);
        }
    }
}
