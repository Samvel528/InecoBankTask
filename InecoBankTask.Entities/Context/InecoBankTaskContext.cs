using InecoBankTask.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace InecoBankTask.Entities.Context;

public partial class InecoBankTaskContext : DbContext
{
    public InecoBankTaskContext()
    {
    }

    public InecoBankTaskContext(DbContextOptions<InecoBankTaskContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ProductInfo> ProductInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=ALIENWARE;Initial Catalog=InecoBankTask;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductInfo>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__ProductI__B40CC6ED8B991C91");

            entity.ToTable("ProductInfo");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
