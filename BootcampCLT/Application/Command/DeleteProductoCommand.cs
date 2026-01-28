using MediatR;

namespace BootcampCLT.Application.Command
{
    public class DeleteProductoCommand : IRequest<bool>
    {
        public int Id { get; }

        public DeleteProductoCommand(int id)
        {
            Id = id;
        }
    }
}
