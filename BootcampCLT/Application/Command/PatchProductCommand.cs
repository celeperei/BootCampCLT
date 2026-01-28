using MediatR;

namespace BootcampCLT.Application.Command
{
    public class PatchProductoCommand : IRequest<ProductoResponse?>
    {
        public int Id { get; }
        public PatchProductoCommand(int id)
        {
            Id = id;
        }
    }
}
