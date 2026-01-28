using MediatR;

namespace BootcampCLT.Application.Command
{
    public class UpdateProductoCommand : IRequest<ProductoResponse?>
    {
        public int Id { get; }
        public UpdateProductoRequest Request { get; }

        public UpdateProductoCommand(int id, UpdateProductoRequest request)
        {
            Id = id;
            Request = request;
        }
    }
}