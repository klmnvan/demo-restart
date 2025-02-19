﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DemoEkzZachet.Models;

public partial class ToursContext : DbContext
{
    public ToursContext()
    {
    }

    public ToursContext(DbContextOptions<ToursContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Hotel> Hotels { get; set; }

    public virtual DbSet<Tour> Tours { get; set; }

    public virtual DbSet<ToursType> ToursTypes { get; set; }

    public virtual DbSet<TypeEntity> Types { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=tours;Username=postgres;Password=1");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("newtable_pk");

            entity.ToTable("country");

            entity.Property(e => e.Code)
                .HasColumnType("character varying")
                .HasColumnName("code");
            entity.Property(e => e.CountryName)
                .HasColumnType("character varying")
                .HasColumnName("country");
        });

        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("hotels_pk");

            entity.ToTable("hotels");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CountOfStars).HasColumnName("count_of_stars");
            entity.Property(e => e.CountryCode)
                .HasColumnType("character varying")
                .HasColumnName("country_code");
            entity.Property(e => e.Description)
                .HasColumnType("character varying")
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");

            entity.HasOne(d => d.CountryCodeNavigation).WithMany(p => p.Hotels)
                .HasForeignKey(d => d.CountryCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("hotels_country_fk");
        });

        modelBuilder.Entity<Tour>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tours_pk");

            entity.ToTable("tours");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasColumnType("character varying")
                .HasColumnName("description");
            entity.Property(e => e.ImagePreview)
                .HasColumnType("character varying")
                .HasColumnName("image_preview");
            entity.Property(e => e.IsActual).HasColumnName("is_actual");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.TicketCount).HasColumnName("ticket_count");
        });

        modelBuilder.Entity<ToursType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tours_type_pk");

            entity.ToTable("tours_type");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.TourId).HasColumnName("tour_id");
            entity.Property(e => e.TypeId).HasColumnName("type_id");

            entity.HasOne(d => d.Tour).WithMany(p => p.ToursTypes)
                .HasForeignKey(d => d.TourId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tours_type_tours_fk");

            entity.HasOne(d => d.Type).WithMany(p => p.ToursTypes)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tours_type_type_fk");
        });

        modelBuilder.Entity<TypeEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("type_pk");

            entity.ToTable("type");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.TypeName)
                .HasColumnType("character varying")
                .HasColumnName("type");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
