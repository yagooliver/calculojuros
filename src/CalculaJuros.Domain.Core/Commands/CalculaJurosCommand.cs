using CalculaJuros.Domain.Core.Validacao;

namespace CalculaJuros.Domain.Core.Commands
{
    public class CalculaJurosCommand : GenericCommand<decimal>
    {
        public CalculaJurosCommand(decimal valor, int meses)
        {
            Valor = valor;
            Meses = meses;
        }

        public decimal Valor { get; private set; }
        public int Meses { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new CalculaJurosValidacao().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
