using System;
using ProjectName.Contract.Classes;
using ProjectName.Contract.Validations;
using System.ComponentModel.DataAnnotations;

namespace ProjectName.Contract.Validations
{
    public class GradeValidationAttribute : ValidationAttribute
    {
        public GradeValidationAttribute()
        {
        }

        public string ErrorMsg { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // add your validation logic, below is just a sample
            if (string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult(ErrorMsg);
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}