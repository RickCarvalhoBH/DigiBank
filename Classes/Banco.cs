namespace DigiBank.Classes
{
    public abstract class Banco
    {
        public Banco()
        {
            this.NomeBanco = "DigiBank";
            this.CodigoBanco = "027";
        }

        public string NomeBanco { get; private set; }
        public string CodigoBanco { get; private set; }
    }
}
