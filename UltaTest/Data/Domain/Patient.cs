using System;

namespace UltaTest.Data.Domains
{
    public class Patient : BaseEntity
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }
    }

    public enum Gender
    {
        NotSpecified,
        Male,
        Female,
    }
}
