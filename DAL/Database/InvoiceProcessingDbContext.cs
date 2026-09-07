using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
