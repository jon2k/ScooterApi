using FluentValidation;
using ScooterApi.Models.v1;

namespace ScooterApi.Validators.v1
{
    public class DataFromScooterModelValidator : AbstractValidator<DataFromScooterModel>
    {
        public DataFromScooterModelValidator()
        {
            RuleFor(x => x.Coordinate)
                .NotNull()
                .WithMessage("The scooter address must be");
            
            RuleFor(x => x.ChargePercent)
                .InclusiveBetween((byte)0, (byte)100)
                .WithMessage("Scooter charge must be at least 0 and no more than 100%");

            RuleFor(x => x.ScooterId)
                .InclusiveBetween(0, int.MaxValue)
                .WithMessage("Scooter address must be at least 0 and no more than Int.MaxValue");
        }
    }
}