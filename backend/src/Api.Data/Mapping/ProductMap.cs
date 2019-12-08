using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
  public class ProductMap : IEntityTypeConfiguration<ProductEntity>
  {
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
      builder.ToTable("products");

      builder.HasKey(p => p.Id);

      builder.HasIndex(p => p.Name).IsUnique();

      builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("name");

      builder.Property(p => p.Amount)
            .IsRequired()
            .HasColumnName("amount");

      builder.Property(p => p.UnityPrice)
            .IsRequired()
            .HasColumnName("unity_price")
            .HasColumnType("decimal(10, 2)");
    }
  }
}
