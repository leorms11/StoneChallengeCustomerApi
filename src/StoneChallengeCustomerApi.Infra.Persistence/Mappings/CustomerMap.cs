using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoneChallengeCustomerApi.Domain.Models;

namespace StoneChallengeCustomerApi.Infra.Persistence.Mappings;
public class CustomerMap : BaseMap<Customer>
{
    public override void ConfigureCustom(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("customers");

        builder.HasKey(x => x.Id)
            .HasName("pk_tb_customers");

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName("name")
            .HasColumnType("varchar(50)");

        builder.Property(x => x.State)
            .IsRequired()
            .HasColumnName("state")
            .HasColumnType("varchar(2)");

        builder.OwnsOne(x => x.Cpf,
            builderAction =>
            {
                builderAction.Property(x => x.Value)
                    .IsRequired()
                    .HasColumnName("cpf")
                    .HasColumnType("bigint");

                builderAction.HasIndex(x => x.Value)
                    .IsUnique();
            });
    }
}
