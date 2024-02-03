using CRUDTest.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDTest.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    }
}
