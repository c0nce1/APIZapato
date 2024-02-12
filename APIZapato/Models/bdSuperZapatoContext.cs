using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace APIZapato.Models
{
    public partial class bdSuperZapatoContext : DbContext
    {
        public bdSuperZapatoContext()
        {
        }

        public bdSuperZapatoContext(DbContextOptions<bdSuperZapatoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categori> Categoria { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;

        //Se pasa la cadena de conexion a Json
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categori>(entity =>
            {
                entity.HasKey(e => e.Idcategoria)
                    .HasName("PK__categori__ED5D47BCEF69961E");

                entity.ToTable("categoria");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.Idproducto)
                    .HasName("PK__Producto__9E7C0D59E9B49DD3");

                entity.ToTable("Producto");

                entity.Property(e => e.CodifoBarra)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Marca)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.oCategoria)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("FK_IDCATEGORIA");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
