using MeuTodo_net5.Models;
using Microsoft.EntityFrameworkCore;

namespace MeuTodo_net5.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() { }
        public DbSet<Todo> Todos { get; set; }
        protected override void OnConfiguring(
           DbContextOptionsBuilder optionsBuilder)
           => optionsBuilder.UseSqlite(connectionString: "DataSource=app.db;Cache=Shared");
    }
}
