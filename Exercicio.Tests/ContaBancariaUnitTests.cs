using Questao1;

namespace Exercicio.Tests
{
    [TestClass]
    public class ContaBancariaUnitTests
    {
        [TestMethod]
        public void CriarConta_DeveCriarComSucesso_QuandoNaoHaDepositoInicial()
        {
            var conta = new ContaBancaria(1234, "João Silva");
            Assert.AreEqual(1234, conta.Numero);
            Assert.AreEqual("João Silva", conta.Titular);
            Assert.AreEqual("Conta 1234, Titular: João Silva, Saldo: $ 0.00", conta.ToString());
        }

        [TestMethod]
        public void CriarConta_DeveCriarComSucesso_QuandoHaDepositoInicial()
        {
            var conta = new ContaBancaria(5678, "Maria Oliveira", 500.00);
            Assert.AreEqual(5678, conta.Numero);
            Assert.AreEqual("Maria Oliveira", conta.Titular);
            Assert.AreEqual("Conta 5678, Titular: Maria Oliveira, Saldo: $ 500.00", conta.ToString());
        }

        [TestMethod]
        public void Deposito_DeveIncrementarSaldo_QuandoValorForPositivo()
        {
            var conta = new ContaBancaria(1234, "Carlos Pereira", 100.00);
            conta.Deposito(150.00);
            Assert.AreEqual("Conta 1234, Titular: Carlos Pereira, Saldo: $ 250.00", conta.ToString());
        }

        [TestMethod]
        public void Saque_DeveDeduzirSaldoComTaxa_QuandoSaldoForSuficiente()
        {
            var conta = new ContaBancaria(4321, "Ana Santos", 300.00);
            conta.Saque(100.00);
            Assert.AreEqual("Conta 4321, Titular: Ana Santos, Saldo: $ 196.50", conta.ToString());
        }

        [TestMethod]
        public void Saque_DevePermitirESaldoNegativo_QuandoSaldoForInsuficiente()
        {
            var conta = new ContaBancaria(9876, "Pedro Gomes", 50.00);
            conta.Saque(100.00);
            Assert.AreEqual("Conta 9876, Titular: Pedro Gomes, Saldo: $ -53.50", conta.ToString());
        }
    }
}
