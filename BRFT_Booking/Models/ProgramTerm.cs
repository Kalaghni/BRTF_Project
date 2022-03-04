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
        [Required(ErrorMessage = "Academic Plan is required.")]
        public string AcadPlan { get; set; }

        [Display(Name = "Program")]
        public ProgramDetail? ProgramDetail { get; set; }
        public int? ProgramDetailID { get; set; }

        [Display(Name = "Student")]
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
