using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using ToDo.Entities.Concrate;

namespace ToDo.DataAccess.Concrete.EntityFramework.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> modelBuilder)
        {
            modelBuilder.ToTable("Categories");
            modelBuilder.Property<int>(x => x.CategoryId).HasColumnName(@"CategoryId").IsRequired(true).UseIdentityColumn().ValueGeneratedOnAdd();
            modelBuilder.Property<string>(x => x.Name).HasColumnName(@"Name").IsRequired(true).ValueGeneratedNever();
        }
    }
}
