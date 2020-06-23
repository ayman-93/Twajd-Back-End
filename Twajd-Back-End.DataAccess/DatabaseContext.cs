using Microsoft.EntityFrameworkCore;
using Twajd_Back_End.Core.Models;

namespace Twajd_Back_End.DataAccess.Repositories
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
    }
}
