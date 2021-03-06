using Microsoft.EntityFrameworkCore;
using ToDo.Core.Entities.Concrete;
using ToDo.DataAccess.Concrete.EntityFramework.Configurations;
using ToDo.Entities.Concrate;

namespace ToDo.DataAccess.Concrete
{
    public class TodoContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-KB74JGP\\SQLEXPRESS;Database=TodoDB;Integrated Security=true;");
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Todo> Todos { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TodoConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new OperationClaimConfiguration());
            modelBuilder.ApplyConfiguration(new UserOperationClaimConfiguration());
        }
    }
}
