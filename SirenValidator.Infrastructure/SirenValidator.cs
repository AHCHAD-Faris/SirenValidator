namespace SirenValidator.Infrastructure
{
    public class SirenValidator : ISirenValidator
    {
        public bool CheckSirenValidity(string siren)
        {
            if (string.IsNullOrEmpty(siren) || siren.Length != 9 || !siren.All(char.IsDigit))
            {
                return false;
            }

            var controlNumber = ComputeControlNumber(siren[..8]);
            return controlNumber == int.Parse(siren.Substring(8, 1));
        }

        public string ComputeFullSiren(string sirenWithoutControlNumber)
        {
            if (string.IsNullOrEmpty(sirenWithoutControlNumber) || sirenWithoutControlNumber.Length != 8 || !sirenWithoutControlNumber.All(char.IsDigit))
            {
                throw new ArgumentException("Invalid SIREN format");
            }

            var controlNumber = ComputeControlNumber(sirenWithoutControlNumber);
            return sirenWithoutControlNumber + controlNumber;
        }

        private static int ComputeControlNumber(string sirenWithoutControlNumber)
        {
            if (sirenWithoutControlNumber.Length != 8)
            {
                throw new ArgumentException("SIREN without control number should have 8 digits.");
            }

            var sum = sirenWithoutControlNumber
                .Select((c, index) => (c - '0') * (index % 2 == 0 ? 2 : 1))
                .Sum(digit => digit > 9 ? digit - 9 : digit);

            return (10 - (sum % 10)) % 10;
        }
    }
}