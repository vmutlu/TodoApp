using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using ToDo.Entities.Concrate;

namespace ToDo.DataAccess.Concrete.EntityFramework.Configurations
{
    public class TodoConfiguration : IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> modelBuilder)
        {
            modelBuilder.ToTable("Todos");
            modelBuilder.Property<int>(x => x.Id).HasColumnName(@"Id").IsRequired(true).UseIdentityColumn().ValueGeneratedOnAdd();
            modelBuilder.Property<string>(x => x.Content).HasColumnName(@"Content").IsRequired(true).ValueGeneratedNever();
            modelBuilder.Property<DateTime?>(x => x.ReminMeDate).HasColumnName(@"ReminMeDate").IsRequired(false).ValueGeneratedNever();
            modelBuilder.Property<DateTime?>(x => x.DueDate).HasColumnName(@"DueDate").IsRequired(false).ValueGeneratedNever();
            modelBuilder.Property<bool>(x => x.IsFavorite).HasColumnName(@"Title").IsRequired(true).ValueGeneratedNever();
            modelBuilder.Property<int>(x => x.CategoryId).HasColumnName(@"CategoryId").IsRequired(true).ValueGeneratedNever();


            modelBuilder.HasOne(x => x.Category).WithMany(op => op.Todos).IsRequired(true).HasForeignKey(x => x.CategoryId);
        }
    }
}
