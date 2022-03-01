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
        [Required(ErrorMessage = "First Name is required")]
        public string FName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage ="Last Name is required")]
        public string LName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
    }
}
