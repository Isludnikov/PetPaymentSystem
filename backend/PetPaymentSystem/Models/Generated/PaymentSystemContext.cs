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

        public virtual DbSet<Merchant> Merchant { get; set; }
        public virtual DbSet<MerchantIpRange> MerchantIpRange { get; set; }
        public virtual DbSet<Operation> Operation { get; set; }
        public virtual DbSet<Operation3ds> Operation3ds { get; set; }
        public virtual DbSet<Processing> Processing { get; set; }
        public virtual DbSet<Session> Session { get; set; }
        public virtual DbSet<Terminal> Terminal { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Merchant>(entity =>
            {
                entity.HasIndex(e => e.Token)
                    .HasName("IX_token")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.FullName)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.MaxTriesToPay).HasColumnType("int(11)");

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

            modelBuilder.Entity<MerchantIpRange>(entity =>
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
                    .WithMany(p => p.MerchantIpRange)
                    .HasForeignKey(d => d.MerchantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MerchantIpRanges_Merchants");
            });

            modelBuilder.Entity<Operation>(entity =>
            {
                entity.HasIndex(e => e.ExternalId)
                    .HasName("IX_OperationId")
                    .IsUnique();

                entity.HasIndex(e => e.SessionId)
                    .HasName("FK_Operation_Session");

                entity.HasIndex(e => e.TerminalId)
                    .HasName("FK_Operation_Terminal");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Amount).HasColumnType("bigint(10)");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.ExpireMonth).HasColumnType("int(11)");

                entity.Property(e => e.ExpireYear).HasColumnType("int(11)");

                entity.Property(e => e.ExternalId)
                    .IsRequired()
                    .HasColumnType("char(15)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.InvolvedAmount).HasColumnType("bigint(10)");

                entity.Property(e => e.MaskedPan)
                    .IsRequired()
                    .HasColumnType("varchar(19)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.OperationStatus)
                    .IsRequired()
                    .HasColumnType("varchar(11)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.OperationType)
                    .IsRequired()
                    .HasColumnType("varchar(29)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.ProcessingOrderId)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.SessionId).HasColumnType("int(11)");

                entity.Property(e => e.TerminalId).HasColumnType("int(11)");

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.Operation)
                    .HasForeignKey(d => d.SessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Operation_Session");

                entity.HasOne(d => d.Terminal)
                    .WithMany(p => p.Operation)
                    .HasForeignKey(d => d.TerminalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Operation_Terminal");
            });

            modelBuilder.Entity<Operation3ds>(entity =>
            {
                entity.HasIndex(e => e.OperationId)
                    .HasName("FK_Operation_Id")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.LocalMd)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.OperationId).HasColumnType("int(11)");

                entity.Property(e => e.RemoteMd)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.HasOne(d => d.Operation)
                    .WithOne(p => p.Operation3ds)
                    .HasForeignKey<Operation3ds>(d => d.OperationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Operation_Id");
            });

            modelBuilder.Entity<Processing>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.LibraryName)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.Namespace)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.ProcessingName)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.HasIndex(e => e.ExternalId)
                    .HasName("IX_SessionIs")
                    .IsUnique();

                entity.HasIndex(e => new { e.MerchantId, e.OrderId })
                    .HasName("IX_Merchant_OrderId")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Amount).HasColumnType("bigint(10)");

                entity.Property(e => e.Currency)
                    .IsRequired()
                    .HasColumnType("char(3)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.ExpireTime).HasColumnType("datetime");

                entity.Property(e => e.ExternalId)
                    .IsRequired()
                    .HasColumnType("char(24)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.FormKey)
                    .HasColumnType("varchar(16)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.FormLanguage)
                    .IsRequired()
                    .HasColumnType("char(3)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.LastFormGenerationTime)
                    .HasColumnType("datetime")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.MerchantId).HasColumnType("int(11)");

                entity.Property(e => e.OrderDescription)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.OrderId)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.SessionType)
                    .IsRequired()
                    .HasColumnType("varchar(11)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.TryCount).HasColumnType("int(11)");

                entity.HasOne(d => d.Merchant)
                    .WithMany(p => p.Session)
                    .HasForeignKey(d => d.MerchantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Session_Merchant");
            });

            modelBuilder.Entity<Terminal>(entity =>
            {
                entity.HasIndex(e => e.MerchantId)
                    .HasName("FK_Terminal_Merchant");

                entity.HasIndex(e => e.ProcessingId)
                    .HasName("FK_Terminal_Processing");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.AccessToken)
                    .HasColumnType("varchar(2048)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.Login)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.MerchantId).HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.Password)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.Priority).HasColumnType("int(11)");

                entity.Property(e => e.ProcessingId).HasColumnType("int(11)");

                entity.Property(e => e.Rule)
                    .HasColumnType("varchar(1024)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.HasOne(d => d.Merchant)
                    .WithMany(p => p.Terminal)
                    .HasForeignKey(d => d.MerchantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Terminal_Merchant");

                entity.HasOne(d => d.Processing)
                    .WithMany(p => p.Terminal)
                    .HasForeignKey(d => d.ProcessingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Terminal_Processing");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
