using FluentValidation;
using static MoviesOnline.Controllers.AuthController;

namespace MoviesOnline.ValidationRules
{
    public class AuthLogValidation : AbstractValidator<UserLogDTO>
    {
        public AuthLogValidation()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("The name cant be empty");

            RuleFor(x => x.PasswordHash).NotEmpty().WithMessage("The password cant be empty");

        }
    }
}
