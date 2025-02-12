namespace Questao5.Infrastructure.Services.Interface
{
    public interface IIdempotencyService
    {
        Task<bool> IsDuplicateRequest(string requestId);
        Task RegisterRequest(string requestId, string result);
    }
}
