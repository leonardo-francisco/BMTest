using BMWeb.DTO;
using FluentValidation;

namespace BMWeb.Validators
{
    public class TravelRouteDTOValidator : AbstractValidator<TravelRouteDTO>
    {
        public TravelRouteDTOValidator()
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
