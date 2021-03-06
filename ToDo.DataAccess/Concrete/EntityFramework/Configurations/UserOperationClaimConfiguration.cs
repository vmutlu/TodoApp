using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo.Core.Entities.Concrete;

namespace ToDo.DataAccess.Concrete.EntityFramework.Configurations
{
    public class UserOperationClaimConfiguration : IEntityTypeConfiguration<UserOperationClaim>
    {
        public void Configure(EntityTypeBuilder<UserOperationClaim> modelBuilder)
        {
            modelBuilder.ToTable("UserOperationClaims");
            modelBuilder.Property<int>(x => x.Id).HasColumnName(@"Id").IsRequired(true).UseIdentityColumn().ValueGeneratedOnAdd();
            modelBuilder.Property<int>(x => x.UserId).HasColumnName(@"UserId").IsRequired(true).ValueGeneratedNever();
            modelBuilder.Property<int>(x => x.OperationClaimId).HasColumnName(@"OperationClaimId").IsRequired(true).ValueGeneratedNever();


            modelBuilder.HasOne(x => x.User).WithMany(op => op.UserOperationClaims).IsRequired(true).HasForeignKey(x => x.UserId);
            modelBuilder.HasOne(x => x.OperationClaim).WithMany(op => op.UserOperationClaims).IsRequired(true).HasForeignKey(x => x.OperationClaimId);
        }
    }
}
