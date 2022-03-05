using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BRTF_Booking.Models
{
    public class Admin
    {
        public int ID { get; set; }

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
        [Required(ErrorMessage = "Role is required!")]
        public string Role { get; set; }
    }
}
