using BRTF_Booking.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BRTF_Booking.Models
{
    public class User
    {
        public User()
        {
            Active = true;
            this.Bookings = new HashSet<Booking>();
        }

        public int ID { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return FName
                    + (string.IsNullOrEmpty(MName) ? " " :
                        (" " + (char?)MName[0] + ". ").ToUpper())
                    + LName;
            }
        }

        [Display(Name = "Student ID")]
        [StringLength(7, ErrorMessage = "Your student ID must be 7 numbers long")]
        [Range(0, 9999999, ErrorMessage = "Your student ID must only contain numerical values.")]
        [Required(ErrorMessage = "User must have a student ID.")]
        public string StudentID { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "User must have a first name.")]
        public string FName { get; set; }

        [Display(Name = "Middle Name")]
        public string? MName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "User must have a last name.")]
        public string LName { get; set; }

        [Display(Name = "Role")]
        public string Role { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "User must have an email.")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }

        public bool Active { get; set; }
        public ICollection<Booking> Bookings { get; set; }

    }
}
