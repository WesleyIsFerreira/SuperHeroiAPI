using FluentValidation;

namespace SuperHeroiAPI.Validators
{
    public class HeroiValidator : AbstractValidator<SuperHeroi>
    {
        public HeroiValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .Length(2,20)
                .WithMessage("Permetido de 2 a 20 caracteres em {PropertyName}")
                .Must(isValidName)
                .WithMessage("Não são permitidos vilões, mude {PropertyName}");

            RuleFor(c => c.FirstName)
                .NotEmpty()
                .MaximumLength(15);

            
            //RouleFor(c => c.principalVilao).SetValidator(VilaoValidator());
            //RouleForEach(c => c.principalVilao).SetValidator(VilaoValidator());

        }

        private bool isValidName(string name)
        {
            string nameHeroi = name.ToLower();
            return nameHeroi != "coringa" && nameHeroi != "pinguim";
            
        }
    }
}
