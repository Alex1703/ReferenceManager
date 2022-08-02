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

        public virtual DbSet<Acceso> Accesos { get; set; }
        public virtual DbSet<Campo> Campos { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Comercial> Comercials { get; set; }
        public virtual DbSet<DetallePerfilAcceso> DetallePerfilAccesos { get; set; }
        public virtual DbSet<DetallePlantillaCampo> DetallePlantillaCampos { get; set; }
        public virtual DbSet<ListaReferencium> ListaReferencia { get; set; }
        public virtual DbSet<Perfil> Perfils { get; set; }
        public virtual DbSet<PerfilAnalistum> PerfilAnalista { get; set; }
        public virtual DbSet<Plantilla> Plantillas { get; set; }
        public virtual DbSet<PlantillaCampo> PlantillaCampos { get; set; }
        public virtual DbSet<RefArrendadorLocal> RefArrendadorLocals { get; set; }
        public virtual DbSet<RefArrendadorViviendum> RefArrendadorVivienda { get; set; }
        public virtual DbSet<RefFamiliar> RefFamiliars { get; set; }
        public virtual DbSet<RefProveedor> RefProveedors { get; set; }
        public virtual DbSet<TipoCampo> TipoCampos { get; set; }
        public virtual DbSet<TipoCliente> TipoClientes { get; set; }
        public virtual DbSet<TipoComunicacion> TipoComunicacions { get; set; }
        public virtual DbSet<TipoReferencium> TipoReferencia { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Zona> Zonas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-EVSN5I0\\SQLEXPRESS;Database=DBReferencias;Trusted_Connection=True;");
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
                    .HasConstraintName("FK__Campo__Fk_TipoCa__5629CD9C");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Cliente");

                entity.Property(e => e.Activio).HasDefaultValueSql("((1))");

                entity.Property(e => e.ClaseCiente)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.DireccionNegocio)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.EstadoCivil)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FkCliente).HasColumnName("Fk_Cliente");

                entity.Property(e => e.FkComercial).HasColumnName("Fk_Comercial");

                entity.Property(e => e.FkTipoCliente).HasColumnName("Fk_TipoCliente");

                entity.Property(e => e.NombreNegocio)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PrimerApellido)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PrimerNombre)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SegundoApellido)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SegundoNombre)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TipoIdentificacion)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.TipoLocal)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TipoNegocio)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.TipoVivienda)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkClienteNavigation)
                    .WithMany(p => p.InverseFkClienteNavigation)
                    .HasForeignKey(d => d.FkCliente)
                    .HasConstraintName("FK__Cliente__Fk_Clie__571DF1D5");

                entity.HasOne(d => d.FkComercialNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.FkComercial)
                    .HasConstraintName("FK__Cliente__Fk_Come__5812160E");

                entity.HasOne(d => d.FkTipoClienteNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.FkTipoCliente)
                    .HasConstraintName("FK__Cliente__Fk_Tipo__59063A47");
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
                    .HasConstraintName("FK__Comercial__Fk_Zo__59FA5E80");
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
                    .HasConstraintName("FK__DetallePe__FK_Ac__5AEE82B9");

                entity.HasOne(d => d.FkPerfilNavigation)
                    .WithMany(p => p.DetallePerfilAccesos)
                    .HasForeignKey(d => d.FkPerfil)
                    .HasConstraintName("FK__DetallePe__Fk_Pe__5BE2A6F2");
            });

            modelBuilder.Entity<DetallePlantillaCampo>(entity =>
            {
                entity.Property(e => e.Activo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(200)
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
                    .HasConstraintName("FK__DetallePl__Fk_Pl__5CD6CB2B");

                entity.HasOne(d => d.FkTipoClienteNavigation)
                    .WithMany(p => p.DetallePlantillaCampos)
                    .HasForeignKey(d => d.FkTipoCliente)
                    .HasConstraintName("FK__DetallePl__Fk_Ti__5DCAEF64");

                entity.HasOne(d => d.FkTipoReferenciaNavigation)
                    .WithMany(p => p.DetallePlantillaCampos)
                    .HasForeignKey(d => d.FkTipoReferencia)
                    .HasConstraintName("FK__DetallePl__Fk_Ti__5EBF139D");
            });

            modelBuilder.Entity<ListaReferencium>(entity =>
            {
                entity.Property(e => e.Activio).HasDefaultValueSql("((1))");

                entity.Property(e => e.FkCliente).HasColumnName("Fk_Cliente");

                entity.Property(e => e.FkTipoComunicacion).HasColumnName("Fk_TipoComunicacion");

                entity.Property(e => e.FkTipoReferencia).HasColumnName("Fk_TipoReferencia");

                entity.Property(e => e.PersonaContacto)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkClienteNavigation)
                    .WithMany(p => p.ListaReferencia)
                    .HasForeignKey(d => d.FkCliente)
                    .HasConstraintName("FK__ListaRefe__Fk_Cl__5FB337D6");

                entity.HasOne(d => d.FkTipoComunicacionNavigation)
                    .WithMany(p => p.ListaReferencia)
                    .HasForeignKey(d => d.FkTipoComunicacion)
                    .HasConstraintName("FK__ListaRefe__Fk_Ti__619B8048");

                entity.HasOne(d => d.FkTipoReferenciaNavigation)
                    .WithMany(p => p.ListaReferencia)
                    .HasForeignKey(d => d.FkTipoReferencia)
                    .HasConstraintName("FK__ListaRefe__Fk_Ti__60A75C0F");
            });

            modelBuilder.Entity<Perfil>(entity =>
            {
                entity.ToTable("Perfil");

                entity.Property(e => e.Activio).HasDefaultValueSql("((1))");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(200)
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

                entity.Property(e => e.FkUsuario).HasColumnName("Fk_Usuario");

                entity.HasOne(d => d.FkUsuarioNavigation)
                    .WithMany(p => p.PerfilAnalista)
                    .HasForeignKey(d => d.FkUsuario)
                    .HasConstraintName("FK__PerfilAna__Fk_Us__628FA481");
            });

            modelBuilder.Entity<Plantilla>(entity =>
            {
                entity.ToTable("Plantilla");

                entity.Property(e => e.Activio).HasDefaultValueSql("((1))");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(200)
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
                    .HasConstraintName("FK__Plantilla__Fk_Ca__6383C8BA");

                entity.HasOne(d => d.FkPlantillaNavigation)
                    .WithMany(p => p.PlantillaCampos)
                    .HasForeignKey(d => d.FkPlantilla)
                    .HasConstraintName("FK__Plantilla__Fk_Pl__6477ECF3");
            });

            modelBuilder.Entity<RefArrendadorLocal>(entity =>
            {
                entity.ToTable("RefArrendadorLocal");

                entity.Property(e => e.CanonArriendo).HasColumnType("money");

                entity.Property(e => e.CantidadEmpleados)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Concepto)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DireccionLocal)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EstadoCivil)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.IncluyeServicios)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.NombreConyuge)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PuntualResponsable)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.QueArrienda)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.QuienVive)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.TiempoArriendo)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.TipoNegocio)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RefArrendadorViviendum>(entity =>
            {
                entity.Property(e => e.Actividad)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.CanonArriendo).HasColumnType("money");

                entity.Property(e => e.Concepto)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DireccionVivienda)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EstadoCivil)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.IncluyeServicios)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.NombreConyuge)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PuntualResponsable)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.QueArrienda)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.QuienVive)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.TiempoArriendo)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RefFamiliar>(entity =>
            {
                entity.ToTable("RefFamiliar");

                entity.Property(e => e.Actividad)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.BarrioNegocio)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CantidadEmpleados)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CantidadHijos)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Concepto)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ConfirmacionNombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DireccionTelefono)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EstadoCivil)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.NombreConyuge)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Parentezco)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.QuienVive)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.TiempoNegocio)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RefProveedor>(entity =>
            {
                entity.ToTable("RefProveedor");

                entity.Property(e => e.Cargo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Concepto)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ConfirmarNombre)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ContadoCredito)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.CupoCredito)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.DireccionProveedor)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FrecuenciaCompra)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PagoCredito)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PlazoCredito)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ProductoVenta)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PromedioCompra)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.TelefonoProveedor)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TiempoVenta)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ValorUltimaCompra)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoCampo>(entity =>
            {
                entity.ToTable("TipoCampo");

                entity.Property(e => e.Activio).HasDefaultValueSql("((1))");

                entity.Property(e => e.Descripcon)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoCliente>(entity =>
            {
                entity.ToTable("TipoCliente");

                entity.Property(e => e.Activio).HasDefaultValueSql("((1))");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(200)
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
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.FkPerfilAnalista).HasColumnName("Fk_PerfilAnalista");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkPerfilAnalistaNavigation)
                    .WithMany(p => p.TipoComunicacions)
                    .HasForeignKey(d => d.FkPerfilAnalista)
                    .HasConstraintName("FK__TipoComun__Fk_Pe__656C112C");
            });

            modelBuilder.Entity<TipoReferencium>(entity =>
            {
                entity.Property(e => e.Activio).HasDefaultValueSql("((1))");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");

                entity.HasIndex(e => e.Correo, "UQ__Usuario__60695A192430E047")
                    .IsUnique();

                entity.HasIndex(e => e.Identificacion, "UQ__Usuario__D6F931E5912536ED")
                    .IsUnique();

                entity.Property(e => e.Activio)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CambioContrasena).HasColumnType("datetime");

                entity.Property(e => e.Correo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EnLinea)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Desconectado')");

                entity.Property(e => e.FkPerfil).HasColumnName("Fk_Perfil");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkPerfilNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.FkPerfil)
                    .HasConstraintName("FK__Usuario__Fk_Perf__66603565");
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
