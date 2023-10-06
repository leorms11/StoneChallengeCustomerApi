using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoneChallengeCustomerApi.Domain.Models;

namespace StoneChallengeCustomerApi.Infra.Persistence.Mappings
{
    public abstract class BaseMap<T> : IEntityTypeConfiguration<T> where T : BaseModel
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            ConfigureDefaultMap(builder);
            ConfigureCustom(builder);
        }

        private void ConfigureDefaultMap(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.Id)
                .IsRequired()
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnName("created_at")
                .HasColumnType("timestamp(6)");

            builder.Property(x => x.LastModifiedAt)
                .IsRequired()
                .HasColumnName("last_modified_at")
                .HasColumnType("timestamp(6)");

            #region Ignores
            builder.Ignore(x => x.IsValid);
            builder.Ignore(x => x.Notifications);
            #endregion
        }

        public abstract void ConfigureCustom(EntityTypeBuilder<T> builder);
    }
}
