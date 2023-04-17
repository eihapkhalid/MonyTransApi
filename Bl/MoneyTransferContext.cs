using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace Bl;

public partial class MoneyTransferContext : DbContext
{
    public MoneyTransferContext()
    {
    }

    public MoneyTransferContext(DbContextOptions<MoneyTransferContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbBankAccount> TbBankAccounts { get; set; }

    public virtual DbSet<TbFinancialTransaction> TbFinancialTransactions { get; set; }

    public virtual DbSet<TbUser> TbUsers { get; set; }

    public virtual DbSet<VwTransWacountDetail> VwTransWacountDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        /*#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server =DESKTOP-IBHOD3E;Database=MoneyTransfer; Trusted_Connection=True;Encrypt=False;");*/
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbBankAccount>(entity =>
        {
            entity.HasKey(e => e.AccountNumber);

            entity.Property(e => e.AccountNumber).HasMaxLength(100);
            entity.Property(e => e.AccountBalance).HasColumnType("decimal(10, 3)");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.TbBankAccounts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbBankAccounts_TbUsers");
        });

        modelBuilder.Entity<TbFinancialTransaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId);

            entity.Property(e => e.TransactionId).HasColumnName("TransactionID");
            entity.Property(e => e.ReceiverAccountNumber).HasMaxLength(100);
            entity.Property(e => e.SenderAccountNumber).HasMaxLength(100);
            entity.Property(e => e.TransactionAmount).HasColumnType("decimal(10, 3)");
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");

            entity.HasOne(d => d.ReceiverAccountNumberNavigation).WithMany(p => p.TbFinancialTransactionReceiverAccountNumberNavigations)
                .HasForeignKey(d => d.ReceiverAccountNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbFinancialTransactions_TbBankAccounts3");

            entity.HasOne(d => d.SenderAccountNumberNavigation).WithMany(p => p.TbFinancialTransactionSenderAccountNumberNavigations)
                .HasForeignKey(d => d.SenderAccountNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbFinancialTransactions_TbBankAccounts2");
        });

        modelBuilder.Entity<TbUser>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Username).HasMaxLength(100);
        });

        modelBuilder.Entity<VwTransWacountDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VwTransWAcountDetails");

            entity.Property(e => e.RaccountBalance)
                .HasColumnType("decimal(10, 3)")
                .HasColumnName("RAccountBalance");
            entity.Property(e => e.RaccountNumber)
                .HasMaxLength(100)
                .HasColumnName("RAccountNumber");
            entity.Property(e => e.ReceiverAccountNumber).HasMaxLength(100);
            entity.Property(e => e.RuserId).HasColumnName("RUserID");
            entity.Property(e => e.SaccountBalance)
                .HasColumnType("decimal(10, 3)")
                .HasColumnName("SAccountBalance");
            entity.Property(e => e.SaccountNumber)
                .HasMaxLength(100)
                .HasColumnName("SAccountNumber");
            entity.Property(e => e.SenderAccountNumber).HasMaxLength(100);
            entity.Property(e => e.SuserId).HasColumnName("SUserID");
            entity.Property(e => e.TransactionAmount).HasColumnType("decimal(10, 3)");
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");
            entity.Property(e => e.TransactionId).HasColumnName("TransactionID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
