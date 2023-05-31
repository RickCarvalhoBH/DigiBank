﻿using DigiBank.Contratos;

namespace DigiBank.Classes
{
    public class Pessoa
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Senha { get; set; }
        public IConta Conta { get; set; }

        public void SetNome(string nome)
        {
            this.Nome = nome;
        }

        public void SetCPF(string cpf)
        {
            this.CPF = cpf;
        }
        
        public void setSenha(string senha)
        {
            this.Senha = senha;
        }
    }
}
