namespace Questao5.Application.Handlers
{
    using MediatR;
    using Questao5.Application.Commands;
    using Questao5.Application.Queries;
    using Dapper;
    using System.Data;
    using System.Threading;
    using System.Threading.Tasks;
    using Questao5.Application.Commands.Responses;
    using Questao5.Infrastructure.Services.Interface;

    public class MovimentarContaHandler : IRequestHandler<MovimentarContaCommand, CommandResult>
    {
        private readonly IDbConnection _dbConnection;
        private readonly IIdempotencyService _idempotencyService;

        public MovimentarContaHandler(IDbConnection dbConnection, IIdempotencyService idempotencyService)
        {
            _dbConnection = dbConnection;
            _idempotencyService = idempotencyService;
        }

        public async Task<CommandResult> Handle(MovimentarContaCommand request, CancellationToken cancellationToken)
        {
            // Lógica de movimentação (similar ao controlador)
            // ...
            return new CommandResult { Sucesso = true, IdMovimento = "UUID" };
        }
    }
}
