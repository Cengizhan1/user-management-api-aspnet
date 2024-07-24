using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Dtos;
using FluentValidation;

namespace Service.Validations
{
    public class UserUpdateReqeustValidator : AbstractValidator<UserUpdateRequest>
    {
        public UserUpdateReqeustValidator()
        {
            RuleFor(user => user.Id)
                .NotEmpty().WithMessage("Id is required.")
                .GreaterThan(0).WithMessage("Id must be greater than 0.");

            RuleFor(user => user.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name cannot be longer than 50 characters.");

            RuleFor(user => user.Surname)
                .NotEmpty().WithMessage("Surname is required.")
                .MaximumLength(50).WithMessage("Surname cannot be longer than 50 characters.");

            RuleFor(user => user.Age)
                .NotEmpty().WithMessage("Age is required.");
        }
    }
}
