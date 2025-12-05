using FluentValidation;
using MoviesOnline.Controllers;
using MoviesOnline.Models;
using static MoviesOnline.Controllers.AuthController;

namespace MoviesOnline.ValidationRules
{
    public class AuthRegValidation : AbstractValidator<UserRegDTO>
    {
         public AuthRegValidation()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("The name cant be empty").MinimumLength(3).WithMessage("The name must be more than 3 characters long")
                .MaximumLength(100).WithMessage("The name must be less than 100 characters long");

            RuleFor(x => x.Email).NotEmpty().WithMessage("The email cant be empty").EmailAddress().WithMessage("Email is incorrent");

            RuleFor(x => x.PasswordHash).NotEmpty().WithMessage("The password cant be empty").MinimumLength(6).WithMessage("The password must be more than 6 characters long")
                .MaximumLength(100).WithMessage("The password must be less than 100 characters long"); ;

        }
    }
}
