using Microsoft.EntityFrameworkCore;
using PedidosApi.Dominio.Dtos;
using PedidosApi.Dominio.Persistencia.Interfaces;
using PedidosApi.Dominio.Persistencia.Modelos;

namespace PedidosApi.Dominio.Persistencia.DbContextMigraciones;

public partial class PedidoDbContext : DbContext, IPedidoDbContext
{
    public PedidoDbContext(DbContextOptions<PedidoDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<PedidoProducto> PedidoProductos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<VistaTotalPedidoDto> VistaTotalPedidos { get; set; }


    public async Task<int> SaveChangesAsync()
    {
        try
        {
            return await base.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            var mensaje = $"existe un campo que infringe las restriciones de la base de datos: {ex.Message}";
            throw new DbUpdateException(mensaje, ex);
        }
        catch (Exception ex)
        {
            var message = $"Ocurrió un error al guardar los cambios: {ex.Message}";
            throw new Exception(message, ex);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<VistaTotalPedidoDto>()
            .ToView("VistaTotalPedidos")
            .HasNoKey();

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Clientes__3214EC07A21CCC78");

            entity.HasIndex(e => e.Email, "UQ__Clientes__A9D10534BA19CE7B").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pedidos__3214EC074B32E743");

            entity.Property(e => e.FechaPedido)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pedidos__Cliente__403A8C7D");
        });

        modelBuilder.Entity<PedidoProducto>(entity =>
        {
            entity.HasKey(e => new { e.PedidoId, e.ProductoId }).HasName("PK__PedidoPr__23F91EDA0C5298B6");

            entity.HasOne(d => d.Pedido).WithMany(p => p.PedidoProductos)
                .HasForeignKey(d => d.PedidoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PedidoPro__Pedid__440B1D61");

            entity.HasOne(d => d.Producto).WithMany(p => p.PedidoProductos)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PedidoPro__Produ__44FF419A");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Producto__3214EC07BA7E0526");

            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
