namespace SirenValidator.Infrastructure
{
    public interface ISirenValidator
    {
        bool CheckSirenValidity(string siren);

        // Returns the full siren from the sirenWithoutControlNumber
        string ComputeFullSiren(string sirenWithoutControlNumber);
    }
}