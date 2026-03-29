using KeyPass_Klimov.Models;
using Microsoft.EntityFrameworkCore;

namespace KeyPass_Klimov.Classes
{
    public class DatabaseManager : DbContext
    {
        public DbSet<Storage> Storages { get; set; }
        public DbSet<User> Users { get; set; }

        public DatabaseManager()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;port=3307;uid=root;pwd=;database=Storage;",
                new MySqlServerVersion(new Version(8, 0, 11)));
        }
    }
}
