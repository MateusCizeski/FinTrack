using System.Text;

namespace FinTrack.Libraries.Utils
{
    public static class TransactionValidator
    {
        public static (bool IsValid, string ErrorMessage) Validate(string name, string value)
        {
            var sb = new StringBuilder();
            bool valid = true;

            if (string.IsNullOrWhiteSpace(name))
            {
                sb.AppendLine("O campo 'Nome' deve ser preenchido!");
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                sb.AppendLine("O campo 'Valor' deve ser preenchido!");
                valid = false;
            }
            else if (!decimal.TryParse(value, out _))
            {
                sb.AppendLine("O campo 'Valor' é inválido!");
                valid = false;
            }

            return (valid, sb.ToString().TrimEnd());
        }
    }
}