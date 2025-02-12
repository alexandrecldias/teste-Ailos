using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;

namespace Questao5.Application.Commands
{
    public class MovimentarContaCommand : IRequest<CommandResult>
    {
        public MovimentacaoRequest Request { get; }

        public MovimentarContaCommand(MovimentacaoRequest request)
        {
            Request = request;
        }
    }
}
