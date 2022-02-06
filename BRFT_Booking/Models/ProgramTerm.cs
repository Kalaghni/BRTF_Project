using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BRFT_Booking.Models
{
    public class ProgramTerm
    {
        public int ID { get; set; }

        [Display(Name ="User")]
        [Required(ErrorMessage = "User is required.")]
        public User User { get; set; }
        public int UserID { get; set; }

        [Display(Name = "Academic Plan")]
        [Required(ErrorMessage = "Academic Plan is required.")]
        public string AcadPlan { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Display(Name = "Start Level")]
        [Required(ErrorMessage = "Start Level is required.")]
        public int StrtLevel { get; set; }

        [Display(Name = "Last Level")]
        [Required(ErrorMessage = "Last Level is required.")]
        public bool LastLevel { get; set; }

        [Display(Name = "Term")]
        [Required(ErrorMessage = "Term is required.")]
        public int Term { get; set; }
    }
}
