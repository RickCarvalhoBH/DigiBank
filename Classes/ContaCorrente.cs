namespace DigiBank.Classes
{
    public class ContaCorrente : Conta
    {
        public ContaCorrente()
        {
            this.NumeroConta = "00" + Conta.NumeroContaSequencial;
        }
    }
}
