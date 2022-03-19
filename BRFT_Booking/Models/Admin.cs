using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BRTF_Booking.Models
{
    public class Admin : IValidatableObject
    {
        public int ID { get; set; }

        [Display(Name = "Admin")]
        public string FullName => $"{FName} {LName}";

        [Display(Name="First Name")]
        [Required(ErrorMessage = "First Name is required!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name need to be between 2 and 50 characters!")]
        public string FName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is required!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Last name need to be between 2 and 100 characters!")]
        public string LName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required!")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter a valid email address!")]
        public string Email { get; set; }

        [Display(Name = "Role")]
        //[Required(ErrorMessage = "Role is required!")]
        public string Role { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Email.Substring(Math.Max(0, Email.Length - 17)) != "niagaracollege.ca")
            {
                yield return new ValidationResult("Email must be a niagaracollege.ca address!", new[] { "Email" });
            }
        }
    }
}
