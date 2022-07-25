using BMAPI.Domain.Entities;
using FluentValidation;

namespace BMAPI.Domain.Validators
{
    public class TravelRouteValidator : AbstractValidator<TravelRoute>
    {
        public TravelRouteValidator()
        {
            RuleFor(x => x.Origem)
                .NotNull().WithMessage("Campo Origem está vazio");
            RuleFor(x => x.Destino)
                .NotNull().WithMessage("Campo Destino está vazio");
            RuleFor(x => x.Valor)
                .NotEmpty().WithMessage("Campo Valor está zerado");
        }
    }
}
