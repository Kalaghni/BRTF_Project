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

        [Display(Name = "Group")]
        public string Group
        {
            get
            {
                try
                {
                    if (this.ProgramDetail.Name == "Acting for TV & Film")
                    {
                        return this.ProgramDetail.ProgramTermGroups.Where(p => p.ProgramDetail.Name == "Acting for TV & Film").FirstOrDefault().Name;
                    }
                    else if (this.StrtLevel == 1)
                    {
                        return "combo1";
                    }
                    else if (this.StrtLevel == 2 && this.ProgramDetail.Name != "Presentation / Radio")
                    {
                        return "combo2";
                    }
                    else if (this.ProgramDetail.ProgramTermGroups.Where(p => p.Level == this.StrtLevel && p.ProgramDetail.ID == this.ProgramDetail.ID).Any())
                    {
                        return this.ProgramDetail.ProgramTermGroups.Where(p => p.Level == this.StrtLevel && p.ProgramDetail.ID == this.ProgramDetail.ID).FirstOrDefault().Name;
                    }
                    else
                    {
                        return "";
                    }
                }
                catch (NullReferenceException ex)
                {
                    return ex.Message;
                }
            }
        }

        [Display(Name = "Academic Plan")]
        [Required(ErrorMessage = "Academic Plan is required!")]
        public string AcadPlan { get; set; }

        [Display(Name = "Program")]
        public int? ProgramDetailID { get; set; }
        public ProgramDetail ProgramDetail { get; set; }

        [Display(Name = "Student")]
        public int? UserID { get; set; }
        public User User { get; set; }

        [Display(Name = "Start Level")]
        [Required(ErrorMessage = "Start Level is required!")]
        [Range(1, 9, ErrorMessage = "Start Level required 1 digit only!")]
        public int StrtLevel { get; set; }

        [Display(Name = "Last Level (Y/N)")]
        [Required(ErrorMessage = "Last Level is required!")]
        [StringLength(1, MinimumLength = 1, ErrorMessage = "Last Level is required!")]
        public string LastLevel { get; set; }

        [Display(Name = "Term")]
        [Required(ErrorMessage = "Term is required!")]
        [Range(1000, 9999, ErrorMessage = "Term number must be 4 digits only!")]
        public int Term { get; set; }
    }
}
