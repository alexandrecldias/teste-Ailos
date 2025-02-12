using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Interfaces
{
    public interface IDatabaseHelper
    {
        Task ExecuteAsync(string queryInsert, object movimento);
        Task<ContaCorrente> GetContaCorrenteAsync(string query, object param);
        Task<IEnumerable<Movimento>> GetMovimentosAsync(string query, object param);
    }
}
