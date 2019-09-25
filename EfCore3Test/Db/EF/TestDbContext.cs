using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EfCore3Test.Db.EF
{
    public partial class TestDbContext : DbContext
    {
        public TestDbContext()
        {
        }

        public TestDbContext(DbContextOptions<TestDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Nodes> Nodes { get; set; }
        public virtual DbSet<NodesData> NodesData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=192.168.0.15;Database=TestDb;User Id=TestUser;Password=TestUser");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Nodes>(entity =>
            {
                entity.HasKey(e => e.NodeId)
                    .HasName("PK__Nodes__6BAE2263EAAA5BB8");

                entity.HasIndex(e => e.Nr)
                    .HasName("ix_nodes_nr")
                    .IsUnique();
            });

            modelBuilder.Entity<NodesData>(entity =>
            {
                entity.HasKey(e => e.DataId)
                    .HasName("PK__NodesDat__9D05303DA9E4DF8D");

                entity.HasOne(d => d.Node)
                    .WithMany(p => p.NodesData)
                    .HasForeignKey(d => d.NodeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__NodesData__NodeI__398D8EEE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
