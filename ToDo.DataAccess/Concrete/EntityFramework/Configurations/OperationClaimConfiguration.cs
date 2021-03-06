using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo.Core.Entities.Concrete;

namespace ToDo.DataAccess.Concrete.EntityFramework.Configurations
{
    public class OperationClaimConfiguration : IEntityTypeConfiguration<OperationClaim>
    {
        public void Configure(EntityTypeBuilder<OperationClaim> modelBuilder)
        {
            modelBuilder.ToTable("OperationClaims");
            modelBuilder.Property<int>(x => x.Id).HasColumnName(@"Id").IsRequired(true).UseIdentityColumn().ValueGeneratedOnAdd();
            modelBuilder.Property<string>(x => x.Name).HasColumnName(@"Name").IsRequired(true).ValueGeneratedNever();
        }
    }
}
