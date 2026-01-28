using BootcampCLT.Infraestructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BootcampCLT.Application.Command
{
    public class PatchProductoHandler
        : IRequestHandler<PatchProductoCommand, ProductoResponse?>
    {
        private readonly PostgresDbContext _postgresDbContext;

        public PatchProductoHandler(PostgresDbContext postgresDbContext)
        {
            _postgresDbContext = postgresDbContext;
        }

        public async Task<ProductoResponse?> Handle(
            PatchProductoCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _postgresDbContext.Productos
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (entity is null)
                return null;

            entity.Activo = true;
            entity.FechaActualizacion = DateTime.UtcNow;

            await _postgresDbContext.SaveChangesAsync(cancellationToken);

            return new ProductoResponse(
                entity.Id,
                entity.Codigo,
                entity.Nombre,
                entity.Descripcion ?? string.Empty,
                (double)entity.Precio,
                entity.Activo,
                entity.CategoriaId,
                entity.FechaCreacion,
                entity.FechaActualizacion,
                entity.CantidadStock
            );
        }
    }
}
