using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ReferenceManager.App.Models
{
    public partial class DBReferenciasContext : DbContext
    {
        public DBReferenciasContext()
        {
        }

        public DBReferenciasContext(DbContextOptions<DBReferenciasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Acceso> Accesos { get; set; } = null!;
        public virtual DbSet<Campo> Campos { get; set; } = null!;
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Comercial> Comercials { get; set; } = null!;
        public virtual DbSet<DetallePerfilAcceso> DetallePerfilAccesos { get; set; } = null!;
        public virtual DbSet<DetallePlantillaCampo> DetallePlantillaCampos { get; set; } = null!;
        public virtual DbSet<Perfil> Perfils { get; set; } = null!;
        public virtual DbSet<PerfilAnalistum> PerfilAnalista { get; set; } = null!;
        public virtual DbSet<Plantilla> Plantillas { get; set; } = null!;
        public virtual DbSet<PlantillaCampo> PlantillaCampos { get; set; } = null!;
        public virtual DbSet<Referencium> Referencia { get; set; } = null!;
        public virtual DbSet<TipoCampo> TipoCampos { get; set; } = null!;
        public virtual DbSet<TipoCliente> TipoClientes { get; set; } = null!;
        public virtual DbSet<TipoComunicacion> TipoComunicacions { get; set; } = null!;
        public virtual DbSet<TipoReferencium> TipoReferencia { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<Zona> Zonas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Acceso>(entity =>
            {
                entity.ToTable("Acceso");

                entity.Property(e => e.Activio).HasDefaultValueSql("((1))");

                entity.Property(e => e.Modulo)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Url).HasMaxLength(500);
            });

            modelBuilder.Entity<Campo>(entity =>
            {
                entity.ToTable("Campo");

                entity.Property(e => e.Activio).HasDefaultValueSql("((1))");

                entity.Property(e => e.FkTipoCampo).HasColumnName("Fk_TipoCampo");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkTipoCampoNavigation)
                    .WithMany(p => p.Campos)
                    .HasForeignKey(d => d.FkTipoCampo)
                    .HasConstraintName("FK__Campo__Fk_TipoCa__534D60F1");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Cliente");

                entity.Property(e => e.Activio).HasDefaultValueSql("((1))");

                entity.Property(e => e.FkComercial).HasColumnName("Fk_Comercial");

                entity.Property(e => e.FkTipoCliente).HasColumnName("Fk_TipoCliente");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkComercialNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.FkComercial)
                    .HasConstraintName("FK__Cliente__Fk_Come__5441852A");

                entity.HasOne(d => d.FkTipoClienteNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.FkTipoCliente)
                    .HasConstraintName("FK__Cliente__Fk_Tipo__5535A963");
            });

            modelBuilder.Entity<Comercial>(entity =>
            {
                entity.ToTable("Comercial");

                entity.Property(e => e.Activio).HasDefaultValueSql("((1))");

                entity.Property(e => e.Cedula)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FkZona).HasColumnName("Fk_Zona");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkZonaNavigation)
                    .WithMany(p => p.Comercials)
                    .HasForeignKey(d => d.FkZona)
                    .HasConstraintName("FK__Comercial__Fk_Zo__5629CD9C");
            });

            modelBuilder.Entity<DetallePerfilAcceso>(entity =>
            {
                entity.ToTable("DetallePerfilAcceso");

                entity.Property(e => e.Activio).HasDefaultValueSql("((1))");

                entity.Property(e => e.FkAcceso).HasColumnName("FK_Acceso");

                entity.Property(e => e.FkPerfil).HasColumnName("Fk_Perfil");

                entity.HasOne(d => d.FkAccesoNavigation)
                    .WithMany(p => p.DetallePerfilAccesos)
                    .HasForeignKey(d => d.FkAcceso)
                    .HasConstraintName("FK__DetallePe__FK_Ac__571DF1D5");

                entity.HasOne(d => d.FkPerfilNavigation)
                    .WithMany(p => p.DetallePerfilAccesos)
                    .HasForeignKey(d => d.FkPerfil)
                    .HasConstraintName("FK__DetallePe__Fk_Pe__5812160E");
            });

            modelBuilder.Entity<DetallePlantillaCampo>(entity =>
            {
                entity.Property(e => e.Activo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EsCliente).HasDefaultValueSql("((1))");

                entity.Property(e => e.FkPlantillaCampos).HasColumnName("Fk_PlantillaCampos");

                entity.Property(e => e.FkTipoCliente).HasColumnName("Fk_TipoCliente");

                entity.Property(e => e.FkTipoReferencia).HasColumnName("Fk_TipoReferencia");

                entity.Property(e => e.NombrePlantilla)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkPlantillaCamposNavigation)
                    .WithMany(p => p.DetallePlantillaCampos)
                    .HasForeignKey(d => d.FkPlantillaCampos)
                    .HasConstraintName("FK__DetallePl__Fk_Pl__59063A47");

                entity.HasOne(d => d.FkTipoClienteNavigation)
                    .WithMany(p => p.DetallePlantillaCampos)
                    .HasForeignKey(d => d.FkTipoCliente)
                    .HasConstraintName("FK__DetallePl__Fk_Ti__59FA5E80");

                entity.HasOne(d => d.FkTipoReferenciaNavigation)
                    .WithMany(p => p.DetallePlantillaCampos)
                    .HasForeignKey(d => d.FkTipoReferencia)
                    .HasConstraintName("FK__DetallePl__Fk_Ti__5AEE82B9");
            });

            modelBuilder.Entity<Perfil>(entity =>
            {
                entity.ToTable("Perfil");

                entity.Property(e => e.Activio).HasDefaultValueSql("((1))");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PerfilAnalistum>(entity =>
            {
                entity.Property(e => e.Activio).HasDefaultValueSql("((1))");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FkPerfil).HasColumnName("Fk_Perfil");

                entity.HasOne(d => d.FkPerfilNavigation)
                    .WithMany(p => p.PerfilAnalista)
                    .HasForeignKey(d => d.FkPerfil)
                    .HasConstraintName("FK__PerfilAna__Fk_Pe__5BE2A6F2");
            });

            modelBuilder.Entity<Plantilla>(entity =>
            {
                entity.ToTable("Plantilla");

                entity.Property(e => e.Activio).HasDefaultValueSql("((1))");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PlantillaCampo>(entity =>
            {
                entity.Property(e => e.EtiquetaCampo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FkCampo).HasColumnName("Fk_Campo");

                entity.Property(e => e.FkPlantilla).HasColumnName("Fk_Plantilla");

                entity.HasOne(d => d.FkCampoNavigation)
                    .WithMany(p => p.PlantillaCampos)
                    .HasForeignKey(d => d.FkCampo)
                    .HasConstraintName("FK__Plantilla__Fk_Ca__5CD6CB2B");

                entity.HasOne(d => d.FkPlantillaNavigation)
                    .WithMany(p => p.PlantillaCampos)
                    .HasForeignKey(d => d.FkPlantilla)
                    .HasConstraintName("FK__Plantilla__Fk_Pl__5DCAEF64");
            });

            modelBuilder.Entity<Referencium>(entity =>
            {
                entity.Property(e => e.Activio).HasDefaultValueSql("((1))");

                entity.Property(e => e.FkCliente).HasColumnName("Fk_Cliente");

                entity.Property(e => e.FkTipoComunicacion).HasColumnName("Fk_TipoComunicacion");

                entity.Property(e => e.FkTipoReferencia).HasColumnName("Fk_TipoReferencia");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkClienteNavigation)
                    .WithMany(p => p.Referencia)
                    .HasForeignKey(d => d.FkCliente)
                    .HasConstraintName("FK__Referenci__Fk_Cl__5EBF139D");

                entity.HasOne(d => d.FkTipoComunicacionNavigation)
                    .WithMany(p => p.Referencia)
                    .HasForeignKey(d => d.FkTipoComunicacion)
                    .HasConstraintName("FK__Referenci__Fk_Ti__60A75C0F");

                entity.HasOne(d => d.FkTipoReferenciaNavigation)
                    .WithMany(p => p.Referencia)
                    .HasForeignKey(d => d.FkTipoReferencia)
                    .HasConstraintName("FK__Referenci__Fk_Ti__5FB337D6");
            });

            modelBuilder.Entity<TipoCampo>(entity =>
            {
                entity.ToTable("TipoCampo");

                entity.Property(e => e.Activio).HasDefaultValueSql("((1))");

                entity.Property(e => e.Descripcon)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoCliente>(entity =>
            {
                entity.ToTable("TipoCliente");

                entity.Property(e => e.Activio).HasDefaultValueSql("((1))");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoComunicacion>(entity =>
            {
                entity.ToTable("TipoComunicacion");

                entity.Property(e => e.Activio).HasDefaultValueSql("((1))");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FkPerfilAnalista).HasColumnName("Fk_PerfilAnalista");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkPerfilAnalistaNavigation)
                    .WithMany(p => p.TipoComunicacions)
                    .HasForeignKey(d => d.FkPerfilAnalista)
                    .HasConstraintName("FK__TipoComun__Fk_Pe__619B8048");
            });

            modelBuilder.Entity<TipoReferencium>(entity =>
            {
                entity.Property(e => e.Activio).HasDefaultValueSql("((1))");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");

                entity.Property(e => e.Activio).HasDefaultValueSql("((1))");

                entity.Property(e => e.CambioContrasena).HasColumnType("datetime");

                entity.Property(e => e.Contrasena)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FkPerfil).HasColumnName("Fk_Perfil");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkPerfilNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.FkPerfil)
                    .HasConstraintName("FK__Usuario__Fk_Perf__628FA481");
            });

            modelBuilder.Entity<Zona>(entity =>
            {
                entity.ToTable("Zona");

                entity.Property(e => e.Activio).HasDefaultValueSql("((1))");

                entity.Property(e => e.Ciudad)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Oficina)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
