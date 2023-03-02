using FluentValidation.Attributes;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using UltaTest.Validations;

namespace UltaTest.Models
{
    [Validator(typeof(PatientValidation))]
    public class PatientModel : BaseModel
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        [DisplayName("Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public int Age
        {
            get
            {
                var age = DateTime.Now.Year - DateOfBirth.Year;
                if (DateTime.Now.DayOfYear < DateOfBirth.DayOfYear)
                    age = age - 1;

                return age;
            }
        }
    }

    public enum Gender
    {
        NotSpecified,
        Male,
        Female,
    }
}
