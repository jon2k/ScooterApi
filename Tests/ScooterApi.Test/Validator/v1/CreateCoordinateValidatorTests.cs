using FluentValidation.TestHelper;
using ScooterApi.Validators.v1;
using Xunit;

namespace ScooterApi.Test.Validator.v1
{
    public class CreateCoordinateValidatorTests
    {
        private readonly DataFromCoordinateValidator _testee;

        public CreateCoordinateValidatorTests()
        {
            _testee = new DataFromCoordinateValidator();
        }

        [Theory]
        [InlineData(-370.0f)]
        [InlineData(370.0f)]
        public void CoordinateX_WhenMoreThen360AndLessThen_360_ShouldNotHaveValidationError(float coordinate)
        {
            _testee.ShouldHaveValidationErrorFor(x => x.X, coordinate)
                .WithErrorMessage("Coordinates must be no less than -360 and no more than 360");
        }
        
        [Theory]
        [InlineData(-370.0f)]
        [InlineData(370.0f)]
        public void CoordinateY_WhenMoreThen360AndLessThen_360_ShouldNotHaveValidationError(float coordinate)
        {
            _testee.ShouldHaveValidationErrorFor(x => x.Y, coordinate)
                .WithErrorMessage("Coordinates must be no less than -360 and no more than 360");
        }
    }
}