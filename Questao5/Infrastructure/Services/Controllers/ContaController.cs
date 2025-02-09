using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Queries;
using Swashbuckle.AspNetCore.Annotations;


namespace Questao5.Infrastructure.Services.Controllers
{
    public class ContaController : Controller
    {
        private readonly IMediator _mediator;

        public ContaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("movimentacao")]
        [SwaggerOperation(Summary = "Realiza uma movimentação na conta corrente.", Description = "Movimentação pode ser crédito ou débito.")]
        [SwaggerResponse(200, "Movimentação registrada com sucesso.", typeof(MovimentacaoResponse))]
        [SwaggerResponse(400, "Erro de validação nos dados da requisição.")]
        public async Task<IActionResult> MovimentarConta([FromBody] MovimentacaoRequest request)
        {
            var command = new MovimentarContaCommand(request);
            var result = await _mediator.Send(command);

            if (!result.Sucesso)
            {
                return BadRequest(new { mensagem = result.Mensagem, tipo = result.TipoErro });
            }

            return Ok(new { idMovimento = result.IdMovimento, mensagem = "Movimentação registrada com sucesso." });
        }

        [HttpGet("saldo")]
        [SwaggerOperation(Summary = "Consulta o saldo atual da conta corrente.", Description = "Retorna o saldo com base nos movimentos registrados.")]
        [SwaggerResponse(200, "Consulta de saldo realizada com sucesso.", typeof(ConsultaSaldoResponse))]
        [SwaggerResponse(400, "Erro de validação nos dados da requisição.")]
        public async Task<IActionResult> ConsultarSaldo([FromQuery] string idContaCorrente)
        {
            var query = new ConsultarSaldoQuery(idContaCorrente);
            var result = await _mediator.Send(query);

            if (!result.Sucesso)
            {
                return BadRequest(new { mensagem = result.Mensagem, tipo = result.TipoErro });
            }

            return Ok(new
            {
                numeroConta = result.NumeroConta,
                nomeTitular = result.NomeTitular,
                dataHoraConsulta = result.DataHoraConsulta,
                saldoAtual = result.SaldoAtual
            });
        }
    }
}
