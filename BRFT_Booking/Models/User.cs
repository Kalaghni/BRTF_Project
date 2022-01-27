using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BRFT_Booking.Models
{
    public class User
    {
        public User()
        {
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
        [Required(ErrorMessage = "User must have a student ID.")]
        public int StudentID { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "User must have a first name.")]
        public string FName { get; set; }

        [Display(Name = "Middle Name")]
        public string? MName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "User must have a last name.")]
        public string LName { get; set; }

        [Display(Name = "Academic Plan")]
        [Required(ErrorMessage = "Academic Plan is required.")]
        public string AcadPlan { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "User must have an email.")]
        public string Email { get; set; }

        [Display(Name = "Start Level")]
        [Required(ErrorMessage = "Start Level is required.")]
        public int StrtLevel { get; set; }

        [Display(Name = "Last Level")]
        [Required(ErrorMessage = "Last Level is required.")]
        public bool LastLevel { get; set; }

        [Display(Name = "Term")]
        [Required(ErrorMessage = "Term is required.")]
        public int Term { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}
