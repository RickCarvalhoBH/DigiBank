using System.Collections.Generic;

using DigiBank.Classes;

namespace DigiBank.Contratos
{
    public interface IConta
    {
        void Deposita(double valor);
        bool Saque(double valor);
        double ConsultaSaldo();
        string GetCodigoBanco();
        string GetNumeroAgencia();
        string GetNumeroConta();
        List<Extrato> Extrato();
    }
}
