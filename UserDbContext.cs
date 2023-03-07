using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiresShopApplication.DbClasses;

namespace TiresShopApplication
{
    public partial class UserDbContext : DbContext
    {
        public UserDbContext()
        {
        }
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<User> User { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString.Get());
            }

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.id_user).HasColumnName("id_user");

                entity.Property(e => e.login).HasColumnName("login");

                entity.Property(e => e.password).HasColumnName("password");

                entity.Property(e => e.type_user).HasColumnName("type_user");
            });
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
