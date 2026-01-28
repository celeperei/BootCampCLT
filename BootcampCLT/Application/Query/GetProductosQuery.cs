using BootcampCLT.Infraestructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BootcampCLT.Application.Query
{
    public class GetProductosQuery : IRequest<IEnumerable<ProductoResponse>>
    {
    }
}
