using FluentValidation;
using ScooterApi.Models.v1;

namespace ScooterApi.Validators.v1
{
    public class DataFromCoordinateValidator : AbstractValidator<Coordinate>
    {
        public DataFromCoordinateValidator()
        {
            RuleFor(x => x.X)
                .InclusiveBetween(-360.0f, 360.0f)
                .WithMessage("Coordinates must be no less than -360 and no more than 360");
            
            RuleFor(x => x.Y)
                .InclusiveBetween(-360.0f, 360.0f)
                .WithMessage("Coordinates must be no less than -360 and no more than 360");
        }
    }
}