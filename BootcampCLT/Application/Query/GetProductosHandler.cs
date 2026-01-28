using BootcampCLT.Infraestructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BootcampCLT.Application.Query
{
    public class GetProductosHandler
        : IRequestHandler<GetProductosQuery, IEnumerable<ProductoResponse>>
    {
        private readonly PostgresDbContext _postgresDbContex;

        public GetProductosHandler(PostgresDbContext postgresDbContex)
        {
            _postgresDbContex = postgresDbContex;
        }

        public async Task<IEnumerable<ProductoResponse>> Handle(
            GetProductosQuery request,
            CancellationToken cancellationToken)
        {
            var entities = await _postgresDbContex.Productos
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return entities.Select(entity => new ProductoResponse(
                Id: entity.Id,
                Codigo: entity.Codigo,
                Nombre: entity.Nombre,
                Descripcion: entity.Descripcion ?? string.Empty,
                Precio: (double)entity.Precio,
                Activo: entity.Activo,
                CategoriaId: entity.CategoriaId,
                FechaCreacion: entity.FechaCreacion,
                FechaActualizacion: entity.FechaActualizacion,
                CantidadStock: entity.CantidadStock
            ));
        }
    }
}