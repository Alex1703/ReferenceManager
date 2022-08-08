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
        public virtual DbSet<Caso> Casos { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Comercial> Comercials { get; set; }
        public virtual DbSet<DetalleComunicacion> DetalleComunicacions { get; set; }
        public virtual DbSet<DetallePerfilAcceso> DetallePerfilAccesos { get; set; }
        public virtual DbSet<GestionReferencium> GestionReferencia { get; set; }
        public virtual DbSet<ListaReferencium> ListaReferencia { get; set; }
        public virtual DbSet<Perfil> Perfils { get; set; }
        public virtual DbSet<RefArrendadorLocal> RefArrendadorLocals { get; set; }
        public virtual DbSet<RefArrendadorViviendum> RefArrendadorVivienda { get; set; }
        public virtual DbSet<RefFamiliar> RefFamiliars { get; set; }
        public virtual DbSet<RefProveedor> RefProveedors { get; set; }
        public virtual DbSet<TipoCliente> TipoClientes { get; set; }
        public virtual DbSet<TipoReferencium> TipoReferencia { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Zona> Zonas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=MSI\\SQLEXPRESS;Database=DBReferencias;Trusted_Connection=True;");
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

            modelBuilder.Entity<Caso>(entity =>
            {
                entity.ToTable("Caso");

                entity.Property(e => e.Estado)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Abierto')");

                entity.Property(e => e.FkCliente).HasColumnName("Fk_Cliente");

                entity.Property(e => e.FkComercial).HasColumnName("Fk_Comercial");

                entity.HasOne(d => d.FkClienteNavigation)
                    .WithMany(p => p.Casos)
                    .HasForeignKey(d => d.FkCliente)
                    .HasConstraintName("FK__Caso__Fk_Cliente__52593CB8");

                entity.HasOne(d => d.FkComercialNavigation)
                    .WithMany(p => p.Casos)
                    .HasForeignKey(d => d.FkComercial)
                    .HasConstraintName("FK__Caso__Fk_Comerci__534D60F1");
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
                    .HasConstraintName("FK__Cliente__Fk_Clie__5441852A");

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

            modelBuilder.Entity<DetalleComunicacion>(entity =>
            {
                entity.ToTable("DetalleComunicacion");

                entity.Property(e => e.Activo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.FkListaReferencia).HasColumnName("Fk_ListaReferencia");

                entity.Property(e => e.FkRefArrendadorLocal).HasColumnName("Fk_RefArrendadorLocal");

                entity.Property(e => e.FkRefArrendadorVivienda).HasColumnName("Fk_RefArrendadorVivienda");

                entity.Property(e => e.FkRefFamiliar).HasColumnName("Fk_RefFamiliar");

                entity.Property(e => e.FkRefProveedor).HasColumnName("Fk_RefProveedor");

                entity.Property(e => e.FkUsuario).HasColumnName("Fk_Usuario");

                entity.HasOne(d => d.FkListaReferenciaNavigation)
                    .WithMany(p => p.DetalleComunicacions)
                    .HasForeignKey(d => d.FkListaReferencia)
                    .HasConstraintName("FK__DetalleCo__Fk_Li__571DF1D5");

                entity.HasOne(d => d.FkRefArrendadorLocalNavigation)
                    .WithMany(p => p.DetalleComunicacions)
                    .HasForeignKey(d => d.FkRefArrendadorLocal)
                    .HasConstraintName("FK__DetalleCo__Fk_Re__59063A47");

                entity.HasOne(d => d.FkRefArrendadorViviendaNavigation)
                    .WithMany(p => p.DetalleComunicacions)
                    .HasForeignKey(d => d.FkRefArrendadorVivienda)
                    .HasConstraintName("FK__DetalleCo__Fk_Re__59FA5E80");

                entity.HasOne(d => d.FkRefFamiliarNavigation)
                    .WithMany(p => p.DetalleComunicacions)
                    .HasForeignKey(d => d.FkRefFamiliar)
                    .HasConstraintName("FK__DetalleCo__Fk_Re__5AEE82B9");

                entity.HasOne(d => d.FkRefProveedorNavigation)
                    .WithMany(p => p.DetalleComunicacions)
                    .HasForeignKey(d => d.FkRefProveedor)
                    .HasConstraintName("FK__DetalleCo__Fk_Re__5BE2A6F2");

                entity.HasOne(d => d.FkUsuarioNavigation)
                    .WithMany(p => p.DetalleComunicacions)
                    .HasForeignKey(d => d.FkUsuario)
                    .HasConstraintName("FK__DetalleCo__Fk_Us__5812160E");
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
                    .HasConstraintName("FK__DetallePe__FK_Ac__5CD6CB2B");

                entity.HasOne(d => d.FkPerfilNavigation)
                    .WithMany(p => p.DetallePerfilAccesos)
                    .HasForeignKey(d => d.FkPerfil)
                    .HasConstraintName("FK__DetallePe__Fk_Pe__5DCAEF64");
            });

            modelBuilder.Entity<GestionReferencium>(entity =>
            {
                entity.Property(e => e.Estado)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FkListaReferencia).HasColumnName("Fk_ListaReferencia");

                entity.Property(e => e.FkUsuario).HasColumnName("Fk_Usuario");

                entity.Property(e => e.FullUrlRef).HasMaxLength(300);

                entity.HasOne(d => d.FkListaReferenciaNavigation)
                    .WithMany(p => p.GestionReferencia)
                    .HasForeignKey(d => d.FkListaReferencia)
                    .HasConstraintName("FK__GestionRe__Fk_Li__5EBF139D");

                entity.HasOne(d => d.FkUsuarioNavigation)
                    .WithMany(p => p.GestionReferencia)
                    .HasForeignKey(d => d.FkUsuario)
                    .HasConstraintName("FK__GestionRe__Fk_Us__5FB337D6");
            });

            modelBuilder.Entity<ListaReferencium>(entity =>
            {
                entity.Property(e => e.Activio).HasDefaultValueSql("((1))");

                entity.Property(e => e.Estado)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Pendiente')");

                entity.Property(e => e.FkCliente).HasColumnName("Fk_Cliente");

                entity.Property(e => e.FkTipoReferencia).HasColumnName("Fk_TipoReferencia");

                entity.Property(e => e.FkUsuario).HasColumnName("Fk_Usuario");

                entity.Property(e => e.PersonaContacto)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkClienteNavigation)
                    .WithMany(p => p.ListaReferencia)
                    .HasForeignKey(d => d.FkCliente)
                    .HasConstraintName("FK__ListaRefe__Fk_Cl__60A75C0F");

                entity.HasOne(d => d.FkTipoReferenciaNavigation)
                    .WithMany(p => p.ListaReferencia)
                    .HasForeignKey(d => d.FkTipoReferencia)
                    .HasConstraintName("FK__ListaRefe__Fk_Ti__628FA481");

                entity.HasOne(d => d.FkUsuarioNavigation)
                    .WithMany(p => p.ListaReferencia)
                    .HasForeignKey(d => d.FkUsuario)
                    .HasConstraintName("FK__ListaRefe__Fk_Us__619B8048");
            });

            modelBuilder.Entity<Perfil>(entity =>
            {
                entity.ToTable("Perfil");

                entity.Property(e => e.Activio).HasDefaultValueSql("((1))");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FkUsuario).HasColumnName("Fk_Usuario");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkUsuarioNavigation)
                    .WithMany(p => p.Perfils)
                    .HasForeignKey(d => d.FkUsuario)
                    .HasConstraintName("FK__Perfil__Fk_Usuar__6383C8BA");
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

            modelBuilder.Entity<TipoReferencium>(entity =>
            {
                entity.Property(e => e.Activio).HasDefaultValueSql("((1))");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Tabla)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Url).HasMaxLength(200);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");

                entity.HasIndex(e => e.Correo, "UQ__Usuario__60695A19778C6F12")
                    .IsUnique();

                entity.HasIndex(e => e.Identificacion, "UQ__Usuario__D6F931E5C55DEAAD")
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
                    .HasConstraintName("FK__Usuario__Fk_Perf__6477ECF3");
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
