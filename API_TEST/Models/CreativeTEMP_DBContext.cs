using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API_TEST.Models
{
    public partial class CreativeTEMP_DBContext : DbContext
    {
        public CreativeTEMP_DBContext()
        {
        }

        public CreativeTEMP_DBContext(DbContextOptions<CreativeTEMP_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<RgFreezer> RgFreezers { get; set; } = null!;
        public virtual DbSet<RgManagerData> RgManagerData { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Group");

                entity.Property(e => e.FreezerName)
                    .HasMaxLength(50)
                    .HasColumnName("FREEZER_NAME");

                entity.Property(e => e.FreezerToken)
                    .HasMaxLength(50)
                    .HasColumnName("FREEZER_TOKEN");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<RgFreezer>(entity =>
            {
                entity.HasKey(e => e.FreezerId);

                entity.ToTable("RG_FREEZER");

                entity.Property(e => e.FreezerId)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("FREEZER_ID");

                entity.Property(e => e.CreativeDatetime)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("CREATIVE_DATETIME");

                entity.Property(e => e.CreativeUserId)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("CREATIVE_USER_ID");

                entity.Property(e => e.FloorId)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("FLOOR_ID");

                entity.Property(e => e.FreezerDesc)
                    .HasMaxLength(200)
                    .HasColumnName("FREEZER_DESC");

                entity.Property(e => e.FreezerName)
                    .HasMaxLength(30)
                    .HasColumnName("FREEZER_NAME");

                entity.Property(e => e.Note)
                    .HasMaxLength(200)
                    .HasColumnName("NOTE");
            });

            modelBuilder.Entity<RgManagerData>(entity =>
            {
                entity.ToTable("RG_ManagerData");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LaboratoryUse)
                    .HasMaxLength(50)
                    .HasColumnName("Laboratory_Use");

                entity.Property(e => e.ManagerName)
                    .HasMaxLength(50)
                    .HasColumnName("Manager_Name");

                entity.Property(e => e.ManagerNum)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("Manager_Num");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
