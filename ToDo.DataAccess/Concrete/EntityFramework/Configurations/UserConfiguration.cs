using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using ToDo.Core.Entities.Concrete;

namespace ToDo.DataAccess.Concrete.EntityFramework.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> modelBuilder)
        {
            modelBuilder.ToTable(@"Users");
            modelBuilder.Property<int>(x => x.Id).HasColumnName(@"Id").IsRequired(true).UseIdentityColumn().ValueGeneratedOnAdd();
            modelBuilder.Property<string>(x => x.FirstName).HasColumnName(@"FirstName").IsRequired(true).ValueGeneratedNever();
            modelBuilder.Property<string>(x => x.LastName).HasColumnName(@"LastName").IsRequired(true).ValueGeneratedNever();
            modelBuilder.Property<byte[]>(x => x.PasswordHash).HasColumnName(@"PasswordHash").IsRequired(true).ValueGeneratedNever();
            modelBuilder.Property<byte[]>(x => x.PasswordSalt).HasColumnName(@"PasswordSalt").IsRequired(true).ValueGeneratedNever();
            modelBuilder.Property<string>(x => x.Email).HasColumnName(@"Email").IsRequired(true).ValueGeneratedNever();
            modelBuilder.Property<bool>(x => x.Status).HasColumnName(@"Status").IsRequired(true).ValueGeneratedNever();
        }
    }
}
