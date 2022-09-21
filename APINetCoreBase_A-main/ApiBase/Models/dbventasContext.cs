using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DevExtremePB_Api.Models
{
    public partial class dbventasContext : DbContext
    {
        public dbventasContext()
        {
        }

        public dbventasContext(DbContextOptions<dbventasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Articulo> Articulos { get; set; } = null!;
        public virtual DbSet<Categorium> Categoria { get; set; } = null!;
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<DetalleIngreso> DetalleIngresos { get; set; } = null!;
        public virtual DbSet<DetalleVentum> DetalleVenta { get; set; } = null!;
        public virtual DbSet<Ingreso> Ingresos { get; set; } = null!;
        public virtual DbSet<Presentacion> Presentacions { get; set; } = null!;
        public virtual DbSet<Proveedor> Proveedors { get; set; } = null!;
        public virtual DbSet<Trabajador> Trabajadors { get; set; } = null!;
        public virtual DbSet<Ventum> Venta { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseSqlServer("Data Source=127.0.0.1;Initial Catalog=dbventas;User ID=sa;Password=1234");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Articulo>(entity =>
            {
                entity.HasKey(e => e.Idarticulo);

                entity.ToTable("articulo");

                entity.Property(e => e.Idarticulo).HasColumnName("idarticulo");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("codigo");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(1024)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Idcategoria).HasColumnName("idcategoria");

                entity.Property(e => e.Idpresentacion).HasColumnName("idpresentacion");

                entity.Property(e => e.Imagen)
                    .HasColumnType("image")
                    .HasColumnName("imagen");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.HasOne(d => d.IdcategoriaNavigation)
                    .WithMany(p => p.Articulos)
                    .HasForeignKey(d => d.Idcategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_articulo_categoria");

                entity.HasOne(d => d.IdpresentacionNavigation)
                    .WithMany(p => p.Articulos)
                    .HasForeignKey(d => d.Idpresentacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_articulo_presentacion");
            });

            modelBuilder.Entity<Categorium>(entity =>
            {
                entity.HasKey(e => e.Idcategoria);

                entity.ToTable("categoria");

                entity.Property(e => e.Idcategoria).HasColumnName("idcategoria");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.Idcliente);

                entity.ToTable("cliente");

                entity.Property(e => e.Idcliente).HasColumnName("idcliente");

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("apellidos");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("direccion");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FechaNacimiento)
                    .HasColumnType("date")
                    .HasColumnName("fecha_nacimiento");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.NumDocumento)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("num_documento");

                entity.Property(e => e.Sexo)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("sexo");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("telefono");

                entity.Property(e => e.TipoDocumento)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("tipo_documento");
            });

            modelBuilder.Entity<DetalleIngreso>(entity =>
            {
                entity.HasKey(e => e.IddetalleIngreso);

                entity.ToTable("detalle_ingreso");

                entity.Property(e => e.IddetalleIngreso).HasColumnName("iddetalle_ingreso");

                entity.Property(e => e.FechaProduccion)
                    .HasColumnType("date")
                    .HasColumnName("fecha_produccion");

                entity.Property(e => e.FechaVencimiento)
                    .HasColumnType("date")
                    .HasColumnName("fecha_vencimiento");

                entity.Property(e => e.Idarticulo).HasColumnName("idarticulo");

                entity.Property(e => e.Idingreso).HasColumnName("idingreso");

                entity.Property(e => e.PrecioCompra)
                    .HasColumnType("money")
                    .HasColumnName("precio_compra");

                entity.Property(e => e.PrecioVenta)
                    .HasColumnType("money")
                    .HasColumnName("precio_venta");

                entity.Property(e => e.StockActual).HasColumnName("stock_actual");

                entity.Property(e => e.StockInicial).HasColumnName("stock_inicial");

                entity.HasOne(d => d.IdarticuloNavigation)
                    .WithMany(p => p.DetalleIngresos)
                    .HasForeignKey(d => d.Idarticulo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_detalle_ingreso_articulo");

                entity.HasOne(d => d.IdingresoNavigation)
                    .WithMany(p => p.DetalleIngresos)
                    .HasForeignKey(d => d.Idingreso)
                    .HasConstraintName("FK_detalle_ingreso_ingreso");
            });

            modelBuilder.Entity<DetalleVentum>(entity =>
            {
                entity.HasKey(e => e.IddetalleVenta);

                entity.ToTable("detalle_venta");

                entity.Property(e => e.IddetalleVenta).HasColumnName("iddetalle_venta");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Descuento)
                    .HasColumnType("money")
                    .HasColumnName("descuento");

                entity.Property(e => e.IddetalleIngreso).HasColumnName("iddetalle_ingreso");

                entity.Property(e => e.Idventa).HasColumnName("idventa");

                entity.Property(e => e.PrecioVenta)
                    .HasColumnType("money")
                    .HasColumnName("precio_venta");

                entity.HasOne(d => d.IddetalleIngresoNavigation)
                    .WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.IddetalleIngreso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_detalle_venta_detalle_ingreso");

                entity.HasOne(d => d.IdventaNavigation)
                    .WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.Idventa)
                    .HasConstraintName("FK_detalle_venta_venta");
            });

            modelBuilder.Entity<Ingreso>(entity =>
            {
                entity.HasKey(e => e.Idingreso);

                entity.ToTable("ingreso");

                entity.Property(e => e.Idingreso).HasColumnName("idingreso");

                entity.Property(e => e.Correlativo)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("correlativo");

                entity.Property(e => e.Estado)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");

                entity.Property(e => e.Idproveedor).HasColumnName("idproveedor");

                entity.Property(e => e.Idtrabajador).HasColumnName("idtrabajador");

                entity.Property(e => e.Igv)
                    .HasColumnType("decimal(4, 2)")
                    .HasColumnName("igv");

                entity.Property(e => e.Serie)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("serie");

                entity.Property(e => e.TipoComprobante)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("tipo_comprobante");

                entity.HasOne(d => d.IdproveedorNavigation)
                    .WithMany(p => p.Ingresos)
                    .HasForeignKey(d => d.Idproveedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ingreso_proveedor");

                entity.HasOne(d => d.IdtrabajadorNavigation)
                    .WithMany(p => p.Ingresos)
                    .HasForeignKey(d => d.Idtrabajador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ingreso_trabajador");
            });

            modelBuilder.Entity<Presentacion>(entity =>
            {
                entity.HasKey(e => e.Idpresentacion);

                entity.ToTable("presentacion");

                entity.Property(e => e.Idpresentacion).HasColumnName("idpresentacion");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.HasKey(e => e.Idproveedor);

                entity.ToTable("proveedor");

                entity.Property(e => e.Idproveedor).HasColumnName("idproveedor");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("direccion");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.NumDocumento)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("num_documento");

                entity.Property(e => e.RazonSocial)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("razon_social");

                entity.Property(e => e.SectorComercial)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("sector_comercial");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("telefono");

                entity.Property(e => e.TipoDocumento)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("tipo_documento");

                entity.Property(e => e.Url)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("url");
            });

            modelBuilder.Entity<Trabajador>(entity =>
            {
                entity.HasKey(e => e.Idtrabajador);

                entity.ToTable("trabajador");

                entity.Property(e => e.Idtrabajador).HasColumnName("idtrabajador");

                entity.Property(e => e.Acceso)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("acceso");

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("apellidos");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("direccion");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FechaNac)
                    .HasColumnType("date")
                    .HasColumnName("fecha_nac");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.NumDocumento)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("num_documento");

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Sexo)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("sexo");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("telefono");

                entity.Property(e => e.Usuario)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("usuario");
            });

            modelBuilder.Entity<Ventum>(entity =>
            {
                entity.HasKey(e => e.Idventa);

                entity.ToTable("venta");

                entity.Property(e => e.Idventa).HasColumnName("idventa");

                entity.Property(e => e.Correlativo)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("correlativo");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");

                entity.Property(e => e.Idcliente).HasColumnName("idcliente");

                entity.Property(e => e.Idtrabajador).HasColumnName("idtrabajador");

                entity.Property(e => e.Igv)
                    .HasColumnType("decimal(4, 2)")
                    .HasColumnName("igv");

                entity.Property(e => e.Serie)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("serie");

                entity.Property(e => e.TipoComprobante)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("tipo_comprobante");

                entity.HasOne(d => d.IdclienteNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.Idcliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_venta_cliente");

                entity.HasOne(d => d.IdtrabajadorNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.Idtrabajador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_venta_trabajador");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
