using BRTF_Booking.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BRTF_Booking.Models
{
    public class User : IValidatableObject
    {
        public int ID { get; set; }

        [Display(Name = "Full Name")]
        public string FullName => FName + (string.IsNullOrEmpty(MName) ? " " :
                        (" " + (char?)MName[0] + ". ").ToUpper()) + LName;

        [Display(Name = "Student ID")]
        [StringLength(7, ErrorMessage = "Your student ID must be 7 numbers long!")]
        [Range(0, 9999999, ErrorMessage = "Your student ID must only contain numerical values!")]
        [Required(ErrorMessage = "User must have a student ID.")]
        public string StudentID { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "User must have a first name.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name need to be between 2 and 50 characters!")]
        public string FName { get; set; }

        [Display(Name = "Middle Name")]
        public string? MName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "User must have a last name.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Last name need to be between 2 and 100 characters!")]
        public string LName { get; set; }

        [Display(Name = "Role")]
        public string Role { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required!")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter a valid email address!")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Program Term")]

        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Program Term")]
        public ProgramTerm? ProgramTerm { get; set; }
        public int? ProgramTermID { get; set; }

        public bool Active { get; set; }
        public ICollection<Booking> Bookings { get; set; }

        public User()
        {
            Active = true;
            this.Bookings = new HashSet<Booking>();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            int output;
            if (Int32.TryParse(FName, out output) == true)
            {
                yield return new ValidationResult("First name can't be numbers!", new[] { "FName" });
            }
            if (Int32.TryParse(MName, out output) == true)
            {
                yield return new ValidationResult("Middle name can't be numbers!", new[] { "MName" });
            }
            if (Int32.TryParse(LName, out output) == true)
            {
                yield return new ValidationResult("Last name can't be numbers!", new[] { "LName" });
            }
            if (Email.Substring(Math.Max(0, Email.Length - 17)) != "niagaracollege.ca")
            {
                yield return new ValidationResult("Email must be a niagaracollege.ca address!", new[] { "Email" });
            }
        }
    }
}
