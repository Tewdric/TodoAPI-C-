using Microsoft.EntityFrameworkCore;
using TodoList.Models; 

namespace TodoList.Data 
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TodoTask> Tasks { get; set; }
    }
}