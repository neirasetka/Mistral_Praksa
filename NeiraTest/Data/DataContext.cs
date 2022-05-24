using Microsoft.EntityFrameworkCore;
using NeiraTest.Models;

namespace NeiraTest.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {

        }
        public DbSet<Character> Characters { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
