using MediatR;
using Questao5.Application.Commands;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Interfaces;
using Questao5.Infrastructure.Services.Interface;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Questao5.Application.Handlers
{
    public class MovimentarContaHandler : IRequestHandler<MovimentarContaCommand, CommandResult>
    {
        private readonly IDatabaseHelper _databaseHelper;
        private readonly IIdempotencyService _idempotencyService;

        public MovimentarContaHandler(IDatabaseHelper databaseHelper, IIdempotencyService idempotencyService)
        {
            _databaseHelper = databaseHelper;
            _idempotencyService = idempotencyService;
        }

        public async Task<CommandResult> Handle(MovimentarContaCommand request, CancellationToken cancellationToken)
        {
            // Verificar se a requisição já foi processada (idempotência)
            if (await _idempotencyService.IsDuplicateRequest(request.Request.IdRequisicao))
            {
                return new CommandResult { Sucesso = false, Mensagem = "Requisição já processada", TipoErro = "DUPLICATE_REQUEST" };
            }

            // Verificar se a conta existe e está ativa
            var queryConta = "SELECT * FROM contacorrente WHERE idcontacorrente = @IdContaCorrente";
            var conta = await _databaseHelper.GetContaCorrenteAsync(queryConta, new { request.Request.IdContaCorrente });

            if (conta == null)
                return new CommandResult { Sucesso = false, Mensagem = "Conta não cadastrada", TipoErro = "INVALID_ACCOUNT" };

            if (conta.Ativo == 0)
                return new CommandResult { Sucesso = false, Mensagem = "Conta inativa", TipoErro = "INACTIVE_ACCOUNT" };

            // Inserir movimentação
            var queryInsert = "INSERT INTO movimento (idmovimento, idcontacorrente, tipomovimento, valor, datamovimento) " +
                              "VALUES (@IdMovimento, @IdContaCorrente, @TipoMovimento, @Valor, @DataMovimento)";
            var movimento = new
            {
                IdMovimento = Guid.NewGuid().ToString(),
                request.Request.IdContaCorrente,
                request.Request.TipoMovimento,
                request.Request.Valor,
                DataMovimento = DateTime.UtcNow
            };

            await _databaseHelper.ExecuteAsync(queryInsert, movimento);

            // Registrar a requisição para idempotência
            await _idempotencyService.RegisterRequest(request.Request.IdRequisicao, movimento.IdMovimento);

            return new CommandResult { Sucesso = true, IdMovimento = movimento.IdMovimento };
        }
    }
}
