using System;
using Microsoft.EntityFrameworkCore;
using PetPaymentSystem.DTO;

namespace PetPaymentSystem.Models.Generated
{
    public partial class PaymentSystemContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Operation>()
                .Property(e => e.OperationStatus)
                .HasConversion(
                    v => v.ToString(),
                    v => (OperationStatus)Enum.Parse(typeof(OperationStatus), v)
                );
            modelBuilder.Entity<Operation>()
                .Property(e => e.OperationType)
                .HasConversion(
                    v => v.ToString(),
                    v => (OperationType)Enum.Parse(typeof(OperationType), v)
                );
            modelBuilder.Entity<Session>()
                .Property(e => e.SessionType)
                .HasConversion(
                    v => v.ToString(),
                    v => (SessionType) Enum.Parse(typeof(SessionType), v));
        }
    }
}
