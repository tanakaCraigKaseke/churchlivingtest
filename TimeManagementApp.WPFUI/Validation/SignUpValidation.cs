using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManagementApp.WPFUI.ViewModels;

namespace TimeManagementApp.WPFUI.Validation
{
    public class SignUpValidation: AbstractValidator<SignUpVM>
    {
        public SignUpValidation()
        {
            RuleFor(a => a.Username)
                .NotEmpty().WithMessage("Required");

            RuleFor(a => a.Password)
                .NotEmpty().WithMessage("Required");

            RuleFor(a => a.FirstName)
                .NotEmpty().WithMessage("Required");

            RuleFor(a => a.LastName)
                .NotEmpty().WithMessage("Required");
        } 
    } 
}
