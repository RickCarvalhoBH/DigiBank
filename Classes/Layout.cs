using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DigiBank.Classes
{
    public class Layout
    {
        private static List<Pessoa> pessoas = new List<Pessoa>();
        private static int opcao = 0;
        
        public static void TelaPrincipal()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.WriteLine("Teste");

            Console.Clear();

            Console.WriteLine("Digite a Opção desejada:");
            Console.WriteLine("============================");
            Console.WriteLine("1 - Criar Conta");
            Console.WriteLine("============================");
            Console.WriteLine("2 - Entrar com CPF e Senha");
            Console.WriteLine("============================");

            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    TelaCriarConta();
                    break;
                case 2:
                    TelaLogin();
                    break;
                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        }

        private static void TelaCriarConta()
        {
            Console.Clear();

            Console.Write("Digite seu nome: ");
            string nome = Console.ReadLine();
            Console.Write("============================\n");
            Console.Write("Digite o CPF: ");
            string cpf = Console.ReadLine();
            Console.Write("============================\n");
            Console.Write("Digite sua senha: ");
            string senha = Console.ReadLine();
            Console.Write("============================\n");

            ContaCorrente contaCorrente = new ContaCorrente();
            Pessoa pessoa = new Pessoa();

            pessoa.SetNome(nome);
            pessoa.SetCPF(cpf);
            pessoa.setSenha(senha);
            pessoa.Conta = contaCorrente;

            pessoas.Add(pessoa);

            Console.Clear();

            Console.WriteLine("Conta cadastrada com sucesso!\n");
            Thread.Sleep(1000);

            TelaContaLogada(pessoa);
        }

        private static void TelaLogin()
        {
            Console.Clear();

            Console.Write("Digite o CPF: ");
            string cpf = Console.ReadLine();
            Console.Write("============================\n");
            Console.Write("Digite sua Senha: ");
            string senha = Console.ReadLine();

            Pessoa pessoa = pessoas.FirstOrDefault(x => x.CPF == cpf && x.Senha == senha);

            if(pessoa != null)
            {
                TelaBoasVindas(pessoa);

                TelaContaLogada(pessoa);
            }
            else
            {
                Console.Clear();

                Console.WriteLine("Usuário não cadastrado\n");
                Thread.Sleep(1000);

                TelaPrincipal();
            }
        }

        private static void TelaBoasVindas(Pessoa pessoa)
        {
            string msgTelaBemVindo =
                $"{pessoa.Nome} | Banco: {pessoa.Conta.GetCodigoBanco()} " +
                $"| Agência: {pessoa.Conta.GetNumeroAgencia()}" +
                $"| Conta: {pessoa.Conta.GetNumeroConta()}";

            Console.WriteLine("");
            Console.WriteLine($"Seja bem vindo, {msgTelaBemVindo}");
            Console.WriteLine("");
        }

        private static void TelaContaLogada(Pessoa pessoa)
        {
            Console.Clear();

            TelaBoasVindas(pessoa);

            Console.Write("Digite a Opção desejada: \n");
            Console.Write("=========================\n");
            Console.Write("1 - Depósito\n");
            Console.Write("=========================\n");
            Console.Write("2 - Saque\n");
            Console.Write("=========================\n");
            Console.Write("3 - Saldo\n");
            Console.Write("=========================\n");
            Console.Write("4 - Extrato\n");
            Console.Write("=========================\n");
            Console.Write("5 - Sair\n");
            Console.Write("=========================\n");

            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    TelaDeposito(pessoa);
                    break;
                case 2:
                    TelaSaque(pessoa);
                    break;
                case 3:
                    TelaConsultaSaldo(pessoa);
                    break;
                case 4:
                    TelaExtrato(pessoa);
                    break;
                case 5:
                    TelaPrincipal();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        }

        private static void TelaDeposito(Pessoa pessoa)
        {
            Console.Clear();

            TelaBoasVindas(pessoa);

            Console.Write("Digite o valor do deposito: ");
            double valor = double.Parse(Console.ReadLine());
            Console.Write("==============================\n");

            pessoa.Conta.Deposita(valor);

            Console.Clear();

            TelaBoasVindas(pessoa);

            Console.Write(" ");
            Console.Write(" ");
            Console.Write("Depósito realizado com sucesso!\n");
            Console.Write("=================================\n");
            Console.Write(" ");

            OpcaoVoltarLogado(pessoa);
        }

        private static void TelaSaque(Pessoa pessoa)
        {
            Console.Clear();

            TelaBoasVindas(pessoa);

            Console.Write("Digite o valor do Saque: ");
            double valor = double.Parse(Console.ReadLine());
            Console.Write("==============================\n");

            bool okSaque = pessoa.Conta.Saque(valor);

            Console.Clear();

            TelaBoasVindas(pessoa);

            Console.Write(" ");
            Console.Write(" ");
            
            if (okSaque)
            {
                Console.Write("Saque realizado com sucesso!\n");
                Console.Write("=================================\n");
            }
            else
            {
                Console.Write("Saldo insuficiente!\n");
                Console.Write("=================================\n");
            }

            OpcaoVoltarLogado(pessoa);
        }

        private static void TelaConsultaSaldo(Pessoa pessoa)
        {
            Console.Clear();

            TelaBoasVindas(pessoa);

            Console.WriteLine($"Seu saldo é de: {pessoa.Conta.ConsultaSaldo()}");
            Console.Write("===============================\n");
            Console.WriteLine("");

            OpcaoVoltarLogado(pessoa);
        }

        private static void TelaExtrato(Pessoa pessoa)
        {
            Console.Clear();

            TelaBoasVindas(pessoa);

            if (pessoa.Conta.Extrato().Any())
            {
                double total = pessoa.Conta.Extrato().Sum(x => x.Valor);

                foreach (Extrato extrato in pessoa.Conta.Extrato())
                {
                    Console.Write($"Data: {extrato.Data.ToString("dd/MM/yyyy HH:mm:ss")}\n");
                    Console.Write($"Tipo de Movimentação: {extrato.Descricao}\n");
                    Console.Write($"Valor: {extrato.Valor}\n");
                    Console.Write("==============================\n");

                }
                
                Console.Write($"Sub Total: {total}\n");
                Console.Write("==============================\n");
            }
            else
            {
                Console.Write("Não há extrato a ser exibido!\n");
                Console.Write("==============================\n");
            }

            OpcaoVoltarLogado(pessoa);
        }

        private static void OpcaoVoltarLogado(Pessoa pessoa)
        {
            Console.WriteLine("\n");
            Console.Write("===============================\n");
            Console.Write("1 - Voltar para minha conta\n");
            Console.Write("===============================\n");
            Console.Write("2 - Sair\n");
            Console.Write("===============================\n");

            opcao = int.Parse(Console.ReadLine());

            if (opcao == 1)
                TelaContaLogada(pessoa);
            else
                TelaPrincipal();
        }

        private static void OpcaoDeslogado()
        {
            Console.Write("Entre com uma opção abaixo\n");
            Console.Write("================================\n");
            Console.Write("1 - Voltar para o menu principal\n");
            Console.Write("2 - Sair\n");
            Console.Write("================================\n");

            opcao = int.Parse(Console.ReadLine());

            if (opcao == 1)
                TelaPrincipal();
            else
            {
                Console.WriteLine("Opção inválida!");
                Console.Write("================================\n");
            }
        }
    }
}
