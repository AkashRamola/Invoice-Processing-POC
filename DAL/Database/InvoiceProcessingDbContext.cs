using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using UCAIDataBase.DataBase;

namespace DAL.Database;

public partial class InvoiceProcessingDbContext : DbContext
{
    public InvoiceProcessingDbContext()
    {
    }

    public InvoiceProcessingDbContext(DbContextOptions<InvoiceProcessingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CustomerInformation> CustomerInformations { get; set; }
    public virtual DbSet<UploadDocument> UploadDocuments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerInformation>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D8BAEFEC46");

            entity.ToTable("CustomerInformation");

            entity.Property(e => e.Createdon)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Password).HasMaxLength(500);
            entity.Property(e => e.UserName).HasMaxLength(100);
        });

        modelBuilder.Entity<UploadDocument>(entity =>
        {
            entity.HasKey(e => e.DocId);

            entity.ToTable("UploadDocument");

            entity.Property(e => e.DocId)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.FileRealName)
                .HasMaxLength(255)
                .IsRequired();

            entity.Property(e => e.FileName)
                .HasMaxLength(255)
                .IsRequired();

            entity.Property(e => e.UploadDate)
                .HasColumnType("datetime2");

            entity.Property(e => e.IsProcessed)
                .HasDefaultValue(false);

            entity.Property(e => e.ExtractedInfo)
                .HasColumnType("nvarchar(max)");

            entity.Property(e => e.MatchedResponse)
                .HasColumnType("nvarchar(max)");

            entity.Property(e => e.IsSafe);

            entity.Property(e => e.source)
                .HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
