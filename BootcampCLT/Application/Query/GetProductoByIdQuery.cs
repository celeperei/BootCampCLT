using MediatR;

namespace BootcampCLT.Application.Query
{
    public record GetProductoByIdQuery(int Id) : IRequest<ProductoResponse?>; //este es el contrato nom;as, defino los parametros y ya
   
}
