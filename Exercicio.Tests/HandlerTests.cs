using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Moq;
using Questao5.Application.Commands;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Handlers;
using Questao5.Application.Queries;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Services.Interface;
using Xunit;
using Questao5.Application.Commands.Requests;
using Assert = Xunit.Assert;
using Questao5.Infrastructure.Interfaces;

namespace Exercicio.Tests
{
    public class HandlerTests
    {

        [Fact]
        public async Task ConsultarSaldoHandler_DeveRetornarSaldoCorreto_QuandoContaExiste()
        {

            var mockDatabaseHelper = new Mock<IDatabaseHelper>();
     
            
            mockDatabaseHelper.Setup(db => db.GetContaCorrenteAsync(It.IsAny<string>(), It.IsAny<object>()))
                            .ReturnsAsync(new ContaCorrente
                            {
                                IdContaCorrente = "123",
                                Numero = 456,
                                Nome = "Test User",
                                Ativo = 1
                            });




            mockDatabaseHelper.Setup(db => db.GetMovimentosAsync(It.IsAny<string>(), It.IsAny<object>()))
                            .ReturnsAsync(new List<Movimento>
                              {
                          new Movimento { TipoMovimento = "C", Valor = 500.00M },
                          new Movimento { TipoMovimento = "D", Valor = 200.00M }
                              });

            var handler = new ConsultarSaldoHandler(mockDatabaseHelper.Object);
            var query = new ConsultarSaldoQuery("123");

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.Sucesso);
            Assert.Equal(300.00M, result.SaldoAtual);  
            Assert.Equal(456, result.NumeroConta);    
        }

    }
}
