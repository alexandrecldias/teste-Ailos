using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Questao1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exercicio.Tests
{
    [TestClass]
    public class ContaBancariaUnitTests
    {
        [TestMethod]
        public void TestCriarContaSemDepositoInicial()
        {
            var conta = new ContaBancaria(1234, "João Silva");
            Assert.AreEqual(1234, conta.Numero);
            Assert.AreEqual("João Silva", conta.Titular);
            Assert.AreEqual("Conta 1234, Titular: João Silva, Saldo: $ 0.00", conta.ToString());
        }

        [TestMethod]
        public void TestCriarContaComDepositoInicial()
        {
            var conta = new ContaBancaria(5678, "Maria Oliveira", 500.00);
            Assert.AreEqual(5678, conta.Numero);
            Assert.AreEqual("Maria Oliveira", conta.Titular);
            Assert.AreEqual("Conta 5678, Titular: Maria Oliveira, Saldo: $ 500.00", conta.ToString());
        }

        [TestMethod]
        public void TestDepositoIncrementaSaldo()
        {
            var conta = new ContaBancaria(1234, "Carlos Pereira", 100.00);
            conta.Deposito(150.00);
            Assert.AreEqual("Conta 1234, Titular: Carlos Pereira, Saldo: $ 250.00", conta.ToString());
        }

        [TestMethod]
        public void TestSaqueDeduzSaldoComTaxa()
        {
            var conta = new ContaBancaria(4321, "Ana Santos", 300.00);
            conta.Saque(100.00);
            Assert.AreEqual("Conta 4321, Titular: Ana Santos, Saldo: $ 196.50", conta.ToString());
        }

        [TestMethod]
        public void TestSaqueComSaldoInsuficiente()
        {
            var conta = new ContaBancaria(9876, "Pedro Gomes", 50.00);
            conta.Saque(100.00);
            Assert.AreEqual("Conta 9876, Titular: Pedro Gomes, Saldo: $ -53.50", conta.ToString());
        }
    }
}
