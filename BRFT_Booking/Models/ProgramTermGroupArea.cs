using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BRTF_Booking.Models
{
    public class ProgramTermGroupArea
    {
        public int ID { get; set; }

        [Display(Name = "Program Term Group")]
        public ProgramTermGroup ProgramTermGroup { get; set; }
        public int? ProgramTermGroupID { get; set; }

        [Display(Name = "Area")]
        public Area Area { get; set; }
        public int? AreaID { get; set; }
    }
}
