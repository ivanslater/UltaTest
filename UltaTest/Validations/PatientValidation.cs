using FluentValidation;
using System;
using UltaTest.Models;

namespace UltaTest.Validations
{
    public class PatientValidation : AbstractValidator<PatientModel>
    {
        public PatientValidation()
        {
            RuleFor(e => e.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(e => e.Address).NotEmpty().WithMessage("Address is required.");
            RuleFor(s => s.Email).NotEmpty().WithMessage("E-mail address is required.").EmailAddress().WithMessage("Invalid e-mail address.");
            RuleFor(p => p.DateOfBirth).Must(BeAValidAge).WithMessage("Invalid date of birth.");
            RuleFor(t => t.Gender).Must(p => p >= 0).WithMessage("Invalid gender selected.");
        }

        protected bool BeAValidAge(DateTime date)
        {
            int currentYear = DateTime.Now.Year;
            int dobYear = date.Year;

            if (dobYear <= currentYear && dobYear > (currentYear - 120))
            {
                return true;
            }

            return false;
        }
    }
}