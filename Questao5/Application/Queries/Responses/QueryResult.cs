namespace Questao5.Application.Queries.Responses
{
    public class QueryResult
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public string TipoErro { get; set; }
        public int NumeroConta { get; set; }
        public string NomeTitular { get; set; }
        public DateTime DataHoraConsulta { get; set; }
        public decimal SaldoAtual { get; set; }
    }
}
