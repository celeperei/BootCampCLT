using MediatR;

namespace BootcampCLT.Application.Command
{
    public class CreateProductoCommand : IRequest<ProductoResponse>
    {
        public CreateProductoRequest Request { get; }

        public CreateProductoCommand(CreateProductoRequest request)
        {
            Request = request;
        }
    }
}