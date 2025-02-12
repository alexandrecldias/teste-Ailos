using MediatR;
using Questao5.Application.Commands;
using Questao5.Application.Queries;
using Dapper;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Interfaces;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Application.Handlers
{
    public class ConsultarSaldoHandler : IRequestHandler<ConsultarSaldoQuery, QueryResult>
    {
        private readonly IDatabaseHelper _databaseHelper;


        public ConsultarSaldoHandler(IDatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        public async Task<QueryResult> Handle(ConsultarSaldoQuery request, CancellationToken cancellationToken)
        {
            // Validar conta
            var query = "SELECT * FROM contacorrente WHERE idcontacorrente = @IdContaCorrente";
            var param = new { request.IdContaCorrente };
            var conta = await _databaseHelper.GetContaCorrenteAsync(query, param);

            if (conta == null)
                return new QueryResult { Sucesso = false, Mensagem = "Conta não cadastrada", TipoErro = "INVALID_ACCOUNT" };

            if (conta.Ativo == 0)
                return new QueryResult { Sucesso = false, Mensagem = "Conta inativa", TipoErro = "INACTIVE_ACCOUNT" };

            var queryMovimentos = "SELECT * FROM movimento WHERE idcontacorrente = @IdContaCorrente";
            var movimentos = await _databaseHelper.GetMovimentosAsync(queryMovimentos, param);


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
