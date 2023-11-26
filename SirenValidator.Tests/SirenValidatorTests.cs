namespace SirenValidator.Tests
{
    using Xunit;
    using Infrastructure;

    public class SirenValidatorTests
    {
        [Theory]
        [InlineData("123456786", true)]
        [InlineData("987654321", false)] // Invalid: Control number mismatch
        [InlineData("abcdefghi", false)] // Invalid: Non-numeric characters
        [InlineData("12345678", false)]  // Invalid: Insufficient length
        [InlineData("", false)]          // Invalid: Empty string
        public void CheckSirenValidity_ValidAndInvalidCases_ReturnsExpectedResult(string siren, bool expectedResult)
        {
            // Arrange
            var validator = new SirenValidator();

            // Act
            var result = validator.CheckSirenValidity(siren);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("12345678", "123456786")]
        [InlineData("98765432", "987654329")]
        public void ComputeFullSiren_ValidInput_ReturnsFullSiren(string sirenWithoutControlNumber, string expectedFullSiren)
        {
            // Arrange
            var validator = new Infrastructure.SirenValidator();

            // Act
            var result = validator.ComputeFullSiren(sirenWithoutControlNumber);

            // Assert
            Assert.Equal(expectedFullSiren, result);
        }

        [Theory]
        [InlineData("abcdefgh", typeof(ArgumentException))] // Invalid: Non-numeric characters
        [InlineData("123", typeof(ArgumentException))]      // Invalid: Insufficient length
        [InlineData("", typeof(ArgumentException))]         // Invalid: Empty string
        public void ComputeFullSiren_InvalidInput_ThrowsException(string sirenWithoutControlNumber, Type expectedExceptionType)
        {
            // Arrange
            var validator = new Infrastructure.SirenValidator();

            // Act & Assert
            Assert.Throws(expectedExceptionType, () => validator.ComputeFullSiren(sirenWithoutControlNumber));
        }
    }
}
