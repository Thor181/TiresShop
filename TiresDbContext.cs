using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TiresShopApplication.DbClasses;

namespace TiresShopApplication
{
    public partial class TiresDbContext : DbContext
    {
        public TiresDbContext()
        {
        }

        public TiresDbContext(DbContextOptions<TiresDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AutoMark> AutoMarks { get; set; } = null!;
        public virtual DbSet<AutoModel> AutoModels { get; set; } = null!;
        public virtual DbSet<AutoModif> AutoModifs { get; set; } = null!;
        public virtual DbSet<AutoTire> AutoTires { get; set; } = null!;
        public virtual DbSet<AutoWheel> AutoWheels { get; set; } = null!;
        public virtual DbSet<DriveUnit> DriveUnits { get; set; } = null!;
        public virtual DbSet<Sale> Sale { get; set; } = null!;
        public virtual DbSet<SaleGoods> SaleGoods { get; set; } = null!;
        public virtual DbSet<SaleGood> SaleGood { get; set; } = null!;
        //public virtual DbSet<User> User { get; set; } = null!;
        public object AutoTire { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString.Get());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AutoMark>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<AutoModel>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdMark).HasColumnName("idMark");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.HasOne(d => d.IdMarkNavigation)
                    .WithMany(p => p.AutoModels)
                    .HasForeignKey(d => d.IdMark)
                    .HasConstraintName("FK_AutoModels_AutoMarks");
            });

            modelBuilder.Entity<AutoModif>(entity =>
            {
                entity.ToTable("AutoModif");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Diameter)
                    .HasMaxLength(50)
                    .HasColumnName("diameter");

                entity.Property(e => e.IdModels).HasColumnName("idModels");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Year).HasColumnName("year");

                entity.HasOne(d => d.IdModelsNavigation)
                    .WithMany(p => p.AutoModifs)
                    .HasForeignKey(d => d.IdModels)
                    .HasConstraintName("FK_AutoModif_AutoModels");
            });

            modelBuilder.Entity<AutoTire>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdDriveUnit).HasColumnName("idDriveUnit");

                entity.Property(e => e.idType).HasColumnName("idType");

                entity.Property(e => e.IdModif).HasColumnName("idModif");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.Season).HasColumnName("season");

                entity.Property(e => e.Load).HasColumnName("loads");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.Color).HasColumnName("color");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.IdDriveUnitNavigation)
                    .WithMany(p => p.AutoTires)
                    .HasForeignKey(d => d.IdDriveUnit)
                    .HasConstraintName("FK_AutoTires_DriveUnit");

                entity.HasOne(d => d.IdModifNavigation)
                    .WithMany(p => p.AutoTires)
                    .HasForeignKey(d => d.IdModif)
                    .HasConstraintName("FK_AutoTires_AutoModif");
            });

            modelBuilder.Entity<AutoWheel>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdDriveUnit).HasColumnName("idDriveUnit");

                entity.Property(e => e.IdModif).HasColumnName("idModif");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.Color).HasColumnName("color");

                entity.Property(e => e.Load).HasColumnName("loads");

                entity.Property(e => e.Season).HasColumnName("season");

                entity.HasOne(d => d.IdDriveUnitNavigation)
                    .WithMany(p => p.AutoWheels)
                    .HasForeignKey(d => d.IdDriveUnit)
                    .HasConstraintName("FK_AutoWheels_DriveUnit");

                entity.HasOne(d => d.IdModifNavigation)
                    .WithMany(p => p.AutoWheels)
                    .HasForeignKey(d => d.IdModif)
                    .HasConstraintName("FK_AutoWheels_AutoModif");
            });

            modelBuilder.Entity<Sale>(entity =>
                {


                    entity.Property(e => e.IdSale).HasColumnName("idSale");

                    entity.Property(e => e.Cost).HasColumnName("cost");

                    entity.Property(e => e.DateOrder).HasColumnName("dateOrder");



                });

            modelBuilder.Entity<SaleGood>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.Cost).HasColumnName("Cost");
                entity.Property(e => e.DateOrder).HasColumnName("DateOrder");
                entity.HasOne(d => d.IdAutoTireNavigation)
                      .WithMany(p => p.SaleGood)
                      .HasForeignKey(d => d.IdAutoTire)
                      .HasConstraintName("FK_SaleGood_AutoTire");
            });

            modelBuilder.Entity<SaleGoods>(entity =>
            {


                entity.Property(e => e.Id_SaleGoods).HasColumnName("id_SaleGoods");

                entity.Property(e => e.Id_Goods).HasColumnName("id_Goods");

                entity.Property(e => e.id_Sale).HasColumnName("id_Sale");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.HasOne(d => d.IdModifNavigation)
                  .WithMany(p => p.SaleGood)
                  .HasForeignKey(d => d.Id_Goods)
                  .HasConstraintName("FK_SaleGoods_AutoTires");

                entity.HasOne(d => d.IdSaleNavigation)
                   .WithMany(p => p.SaleGood)
                   .HasForeignKey(d => d.id_Sale)
                   .HasConstraintName("FK_SaleGoods_Sale");


            });
            //modelBuilder.Entity<User>(entity =>
            //{
            //    entity.Property(e => e.id_user).HasColumnName("id_user");

            //    entity.Property(e => e.login).HasColumnName("login");

            //    entity.Property(e => e.password).HasColumnName("password");

            //    entity.Property(e => e.type_user).HasColumnName("type_user");
            //});

            modelBuilder.Entity<DriveUnit>(entity =>
            {

                entity.ToTable("DriveUnit");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
