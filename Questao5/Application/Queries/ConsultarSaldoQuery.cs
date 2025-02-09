using MediatR;
using Questao5.Application.Queries.Responses;

namespace Questao5.Application.Queries
{
    public class ConsultarSaldoQuery : IRequest<QueryResult>
    {
        public string IdContaCorrente { get; }

        public ConsultarSaldoQuery(string idContaCorrente)
        {
            IdContaCorrente = idContaCorrente;
        }
    }
}
