using Questao5.Domain.Entities;
using Questao5.Infrastructure.Interfaces;
using System.Data;
using Dapper;

namespace Questao5.Infrastructure.Sqlite
{
    public class DatabaseHelper : IDatabaseHelper
    {
        private readonly IDbConnection _dbConnection;

        public DatabaseHelper(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<ContaCorrente> GetContaCorrenteAsync(string query, object param)
        {
            return await _dbConnection.QueryFirstOrDefaultAsync<ContaCorrente>(query, param);
        }

        public async Task<IEnumerable<Movimento>> GetMovimentosAsync(string query, object param)
        {
            return await _dbConnection.QueryAsync<Movimento>(query, param);
        }

        public async Task ExecuteAsync(string queryInsert, object movimento)
        {
            await _dbConnection.ExecuteAsync(queryInsert, movimento);
        }

    }
}
