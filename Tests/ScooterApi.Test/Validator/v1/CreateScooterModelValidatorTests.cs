using System;
using FluentValidation.TestHelper;
using ScooterApi.Models.v1;
using ScooterApi.Validators.v1;
using Xunit;

namespace ScooterApi.Test.Validator.v1
{
    public class CreateScooterModelValidatorTests
    {
        private readonly DataFromScooterModelValidator _testee;

        public CreateScooterModelValidatorTests()
        {
            _testee = new DataFromScooterModelValidator();
        }

        [Theory]
        [InlineData((byte)101)]
        public void ChargePercent_WhenMoreThen100_WhenLessThen0_ShouldHaveValidationError(byte chargePercent)
        {
            _testee.ShouldHaveValidationErrorFor(x => x.ChargePercent, chargePercent).WithErrorMessage("Scooter charge must be at least 0 and no more than 100%");
        }

        [Theory]
        [InlineData(-1)]
        public void ScooterId_WhenLessThen0_ShouldHaveValidationError(int scooterId)
        {
            _testee.ShouldHaveValidationErrorFor(x => x.ScooterId, scooterId).WithErrorMessage("Scooter address must be at least 0 and no more than Int.MaxValue");
        }
        
    }
}