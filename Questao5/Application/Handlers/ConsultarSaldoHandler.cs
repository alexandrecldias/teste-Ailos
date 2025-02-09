using MediatR;
using Questao5.Application.Commands;
using Questao5.Application.Queries;
using Dapper;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Entities;

namespace Questao5.Application.Handlers
{
    public class ConsultarSaldoHandler : IRequestHandler<ConsultarSaldoQuery, QueryResult>
    {
        private readonly IDbConnection _dbConnection;

        public ConsultarSaldoHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<QueryResult> Handle(ConsultarSaldoQuery request, CancellationToken cancellationToken)
        {
            // Validar conta
            var conta = await _dbConnection.QueryFirstOrDefaultAsync<ContaCorrente>(
                "SELECT * FROM contacorrente WHERE idcontacorrente = @IdContaCorrente",
                new { request.IdContaCorrente });

            if (conta == null)
                return new QueryResult { Sucesso = false, Mensagem = "Conta não cadastrada", TipoErro = "INVALID_ACCOUNT" };

            if (conta.Ativo == 0)
                return new QueryResult { Sucesso = false, Mensagem = "Conta inativa", TipoErro = "INACTIVE_ACCOUNT" };

            // Calcular saldo
            var movimentos = await _dbConnection.QueryAsync<Movimento>(
                "SELECT * FROM movimento WHERE idcontacorrente = @IdContaCorrente",
                new { request.IdContaCorrente });

            var saldo = movimentos.Where(m => m.TipoMovimento == "C").Sum(m => m.Valor) -
                        movimentos.Where(m => m.TipoMovimento == "D").Sum(m => m.Valor);

            return new QueryResult
            {
                Sucesso = true,
                NumeroConta = conta.Numero,
                NomeTitular = conta.Nome,
                DataHoraConsulta = DateTime.UtcNow,
                SaldoAtual = saldo
            };
        }
    }
}
