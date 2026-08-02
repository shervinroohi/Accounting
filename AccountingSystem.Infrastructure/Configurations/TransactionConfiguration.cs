using AccountingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountingSystem.Infrastructure.Persistence.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Amount)
                .HasColumnType("decimal(18,4)");

            builder.Property(x => x.TransactionDate)
                .IsRequired();

            builder.Property(x => x.Type)
                .HasConversion<byte>()
                .HasColumnType("tinyint");

            builder.Property(x => x.Status)
                .HasConversion<byte>()
                .HasColumnType("tinyint");

            builder.Property(x => x.IsDelete)
                .HasDefaultValue(false);

            builder.Property(x => x.Description)
                .HasMaxLength(500)              
                .IsRequired(false);
        }
    }
}