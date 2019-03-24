using Microsoft.EntityFrameworkCore;
namespace consolAPP.Model
{
    public class PersonDbContext: DbContext
    {
        public DbSet<Person> persons { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFGetStarted.ConsoleApp.NewDb;Trusted_Connection=True;");
        }

    }
}