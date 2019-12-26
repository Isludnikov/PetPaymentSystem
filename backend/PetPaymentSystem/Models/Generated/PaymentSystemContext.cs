using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PetPaymentSystem.Models.Generated
{
    public partial class PaymentSystemContext : DbContext
    {
        public PaymentSystemContext(DbContextOptions<PaymentSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MerchantIpRanges> MerchantIpRanges { get; set; }
        public virtual DbSet<Merchants> Merchants { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MerchantIpRanges>(entity =>
            {
                entity.HasIndex(e => e.MerchantId)
                    .HasName("FK_MerchantIpRanges_Merchants");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Iprange)
                    .IsRequired()
                    .HasColumnName("IPRange")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.MerchantId).HasColumnType("int(11)");

                entity.HasOne(d => d.Merchant)
                    .WithMany(p => p.MerchantIpRanges)
                    .HasForeignKey(d => d.MerchantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MerchantIpRanges_Merchants");
            });

            modelBuilder.Entity<Merchants>(entity =>
            {
                entity.HasIndex(e => e.Token)
                    .HasName("IX_token")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.FullName)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.ShortName)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.SignKey)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasColumnType("varchar(32)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
