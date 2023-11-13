using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManagementApp.Core.DataTransferObjects;
using TimeManagementApp.WPFUI.ViewModels;

namespace TimeManagementApp.WPFUI.Validation
{
    public class LoginValidation : AbstractValidator<SignInVM>
    {
        public LoginValidation()
        {
            RuleFor(a => a.Username)
                .NotEmpty().WithMessage("Required");

            RuleFor(a => a.Password)
                .NotEmpty().WithMessage("Required");
        } 
    }
}
