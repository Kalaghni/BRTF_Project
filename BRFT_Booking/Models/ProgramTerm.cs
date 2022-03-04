using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BRTF_Booking.Models
{
    public class ProgramTerm
    {
        public int ID { get; set; }

        [Display(Name = "Academic Plan")]
        [Required(ErrorMessage = "Academic Plan is required.")]
        public string AcadPlan { get; set; }

        [Display(Name = "Program")]
        [Required(ErrorMessage = "Program is required.")]
        public ProgramDetail ProgramDetail { get; set; }
        public int ProgramDetailID { get; set; }

        [Display(Name = "Student")]
        [Required(ErrorMessage = "Student is required.")]
        public User? User { get; set; }
        public int? UserID { get; set; }

        [Display(Name = "Start Level")]
        [Required(ErrorMessage = "Start Level is required.")]
        public int StrtLevel { get; set; }

        [Display(Name = "Last Level")]
        [Required(ErrorMessage = "Last Level is required.")]
        public string LastLevel { get; set; }

        [Display(Name = "Term")]
        [Required(ErrorMessage = "Term is required.")]
        public int Term { get; set; }

    }
}
