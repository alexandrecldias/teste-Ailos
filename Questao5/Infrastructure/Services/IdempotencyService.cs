using Questao5.Infrastructure.Services.Interface;
using System.Collections.Concurrent;

namespace Questao5.Infrastructure.Services
{
    public class IdempotencyService : IIdempotencyService
    {
        private static readonly ConcurrentDictionary<string, string> _requests = new();

        public Task<bool> IsDuplicateRequest(string requestId)
        {
            return Task.FromResult(_requests.ContainsKey(requestId));
        }

        public Task RegisterRequest(string requestId, string result)
        {
            _requests.TryAdd(requestId, result);
            return Task.CompletedTask;
        }
    }
}
