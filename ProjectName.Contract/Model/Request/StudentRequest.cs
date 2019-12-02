using System;
using System.ComponentModel.DataAnnotations;
using ProjectName.Contract.Classes;
using ProjectName.Contract.Model.Data;
using ProjectName.Contract.Validations;

namespace ProjectName.Contract.Model.Request
{
    public class StudentRequest
    {
        public long StudentKey { get; set; }

        [Required(ErrorMessage = "FirstName is Required")]
        [MaxLength(ErrorMessage = "Length should be lesser than or equal to 25 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is Required")]
        [MaxLength(ErrorMessage = "Length should be lesser than or equal to 25 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Standard is Required")]
        [MaxLength(ErrorMessage = "Length should be lesser than or equal to 25 characters")]
        [GradeValidation]
        public string Standard { get; set; }
    }
}